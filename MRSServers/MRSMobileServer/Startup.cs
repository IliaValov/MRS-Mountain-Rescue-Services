using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


using MRSMobile.Data;
using MRSMobile.Data.Models;
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
using MRSMobileServer.ViewModels.Account;
using MRSMobileServer.Areas.Mobile.Views.Location;
using System.Reflection;
using MRS.Services.MrsMobileServices;
using MRS.Services.MrsMobileServices.Contracts;

namespace MRSMobileServer
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
              UseSqlServer(this.
              Configuration.
              GetConnectionString("DefaultConnection")));

            // ===== Add Identity ========


            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JwtTokenValidation:Secret"]));

            services.Configure<TokenProviderOptions>(opts =>
            {
                opts.Audience = this.Configuration["JwtTokenValidation:Audience"];
                opts.Issuer = this.Configuration["JwtTokenValidation:Issuer"];
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
                        ValidIssuer = this.Configuration["JwtTokenValidation:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = this.Configuration["JwtTokenValidation:Audience"],
                        ValidateLifetime = true,
                    };
                });

            services
                .AddIdentity<MrsMobileUser, MrsMobileRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<MrsMobileDbContext>()
                .AddUserStore<MrsMobileUserStore>()
                .AddRoleStore<MrsMobileRoleStore>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserStore<MrsMobileUser>, MrsMobileUserStore>();
            services.AddTransient<IRoleStore<MrsMobileRole>, MrsMobileRoleStore>();

            services.AddSingleton(this.Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MrsMobileDbContext dbContext)
        {
            AutoMapperConfig.RegisterMappings(typeof(CreateLocationBindingModel).GetTypeInfo().Assembly);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseJwtBearerTokens(
              app.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>(),
              PrincipalResolver);

            app.UseHttpsRedirection();

            app.UseMvc();


        }


        private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        {
            var phone = context.Request.Form["phonenumber"];
            var token = context.Request.Form["token"];

            using (var dbContext = new MrsMobileDbContext())
            {
                if(dbContext.MobileSmsAuthantications.SingleOrDefault(x => x.Token == token) == null)
                {
                    return null;
                }
            }

            var userManager = context.RequestServices.GetRequiredService<UserManager<MrsMobileUser>>();
            var user = await userManager.FindByNameAsync(phone);
            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);

            var identity = new GenericIdentity(phone, "Token");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            return new GenericPrincipal(identity, roles.ToArray());
        }
    }
}
