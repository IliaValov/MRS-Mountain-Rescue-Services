using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mrs.Spa.LocalData;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Services.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using MRS.Spa.Data.Seeding;
using MRS.Spa.Web.Hubs;
using MRS.Web.Infrastructure.Middlewares.Auth;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Spa.Web
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
            services.AddDbContext<MrsSpaDbContext>
             (options => options.
             UseSqlServer(
             Configuration.
             GetConnectionString("DefaultConnection")));

            services.AddDbContext<MrsMobileDbContext>
              (options => options.
              UseSqlServer(
              Configuration.
              GetConnectionString("MobileDbConnection")));

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
                .AddIdentity<MrsSpaUser, MrsSpaRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<MrsSpaDbContext>()
                .AddUserStore<MrsSpaUserStore>()
                .AddRoleStore<MrsSpaRoleStore>()
                .AddDefaultTokenProviders();

            services.AddSignalR(o => {
                o.EnableDetailedErrors = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddTransient<ISmsAuthanticationService, SmsAuthanticationService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IUserStore<MrsSpaUser>, MrsSpaUserStore>();
            services.AddTransient<IRoleStore<MrsSpaRole>, MrsSpaRoleStore>();

            services.AddSingleton(Configuration);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(LocationCreateBindingModel).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MrsSpaDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                //MrsSpaDbContextSeeder.Seed(dbContext, serviceScope.ServiceProvider, this.Configuration);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseJwtBearerTokens(
              app.ApplicationServices.GetRequiredService<IOptions<TokenProviderOptions>>(),
              PrincipalResolver);


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            app.UseSignalR(
             routes =>
             {
                 routes.MapHub<UserLocationsHub>("/userlocations");
             });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        {
            var username = context.Request.Form["username"];

            var userManager = context.RequestServices.GetRequiredService<UserManager<MrsSpaUser>>();
            var user = await userManager.FindByNameAsync(username);
            if (user == null || user.IsDeleted)
            {
                return null;
            }

            var password = context.Request.Form["password"];

            var isValidPassword = await userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
            {
                return null;
            }

            var roles = await userManager.GetRolesAsync(user);

            var identity = new GenericIdentity(username, "Token");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

            return new GenericPrincipal(identity, roles.ToArray());
        }
    }
}
