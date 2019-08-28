using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;

using MRS.Web.Infrastructure.Middlewares.Auth;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MRS.Common.Mapping;
using System.Reflection;
using MRS.Web.Infrastructure;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Services.Contracts;
using MRS.Services;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Services.Mobile.Data;
using MRS.Mobile.Data.Seeding;

namespace MRS.Mobile.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MrsMobileDbContext>
              (options => options.
              UseSqlServer(
              Configuration.
              GetConnectionString("DefaultConnection")));

            var smsSettingsSection = Configuration.GetSection("SmsValidation");
            services.Configure<SmsOptions>(smsSettingsSection);

            // ===== Add Identity ========

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtTokenValidation:Secret"]));

            services.Configure<TokenProviderOptions>(opts =>
            {
                opts.Audience = Configuration["JwtTokenValidation:Audience"];
                opts.Issuer = Configuration["JwtTokenValidation:Issuer"];
                opts.Path = "/api/account/login";
                opts.Expiration = TimeSpan.FromDays(15);
                opts.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            services
                .AddAuthentication()
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JwtTokenValidation:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtTokenValidation:Audience"],
                        ValidateLifetime = true,
                    };
                });

            services
                .AddIdentity<MrsMobileUser, MrsMobileRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<MrsMobileDbContext>()
                .AddUserStore<MrsMobileUserStore>()
                .AddRoleStore<MrsMobileRoleStore>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Add Services
            services.AddTransient<ISmsAuthanticationService, SmsAuthanticationService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISmsService, SmsService>();

            services.AddTransient<IUserStore<MrsMobileUser>, MrsMobileUserStore>();
            services.AddTransient<IRoleStore<MrsMobileRole>, MrsMobileRoleStore>();

            services.AddSingleton(Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Configure Automapper
            AutoMapperConfig.RegisterMappings(typeof(LocationCreateBindingModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MrsMobileDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                MrsMobileDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Add JwtBearer Token
            app.UseJwtBearerTokens(
              app.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>(),
              PrincipalResolver);

            //app.UseHttpsRedirection();

            app.UseMvc();
        }


        private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        {
            string phoneNumber = context.Request.Form["phonenumber"];
            string smsVerificationCode = context.Request.Form["verificationcode"];
            string token = context.Request.Form["token"];

            using (var dbContext = context.RequestServices.GetRequiredService<MrsMobileDbContext>())
            {
                var mobileAuth = dbContext.MobileSmsAuthantications
                    .Include(x => x.User)
                    .SingleOrDefault(x => x.Token == token &&
                    x.User.UserName == phoneNumber &&
                    x.AuthanticationCode == smsVerificationCode);

                if (mobileAuth == null || mobileAuth.ExpiredAt < DateTime.UtcNow || mobileAuth.IsUsed)
                {
                    return null;
                }

                mobileAuth.IsUsed = true;
                dbContext.Update(mobileAuth);
                await dbContext.SaveChangesAsync();

                var userManager = context.RequestServices.GetRequiredService<UserManager<MrsMobileUser>>();
                var user = await userManager.FindByNameAsync(phoneNumber);
                if (user == null || user.IsDeleted)
                {
                    return null;
                }

                var roles = await userManager.GetRolesAsync(user);

                var identity = new GenericIdentity(phoneNumber, "Token");
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

                return new GenericPrincipal(identity, roles.ToArray());
            }
        }
    }
}
