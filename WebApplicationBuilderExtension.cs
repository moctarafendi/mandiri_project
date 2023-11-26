

using mandiri_project.Entities;
using mandiri_project.Interfaces;
using mandiri_project.Services;
using mandiri_project.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace mandiri_project
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            services.AddHealthChecks();
            services.AddSession();
            services.AddControllers();
            //services.AddRazorPages();
            services.AddHttpClient();

            var jwtTokenConfig = configuration.GetSection(JwtConfig.ConfigName).Get<JwtConfig>();
            services.AddSingleton(jwtTokenConfig);

            var appLaunchSettings = configuration.GetSection(AppLaunchSettings.ConfigName).Get<AppLaunchSettings>();
            services.AddSingleton(appLaunchSettings);

            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DB")));

            // Configure JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JWT_OR_COOKIE";
                options.DefaultChallengeScheme = "JWT_OR_COOKIE";
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtTokenConfig.Issuer, // Replace with your issuer
                    ValidAudience = jwtTokenConfig.Audience, // Replace with your audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret)) // Replace with your secret key
                };
            })
            .AddCookie(options =>
             {
                 options.LoginPath = "/Account/Index"; // Set the login page
                 options.AccessDeniedPath = "/Account/AccessDenied"; // Set the access denied page
             })
            // this is the key piece!
            .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                // runs on each request
                options.ForwardDefaultSelector = context =>
                {
                    // filter by auth type
                    string authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return JwtBearerDefaults.AuthenticationScheme; ;

                    // otherwise always check for cookie auth
                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            });

            services.AddHttpContextAccessor();
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<UserIdentityService>();
            services.AddTransient<IApplicationUser, ApplicationUserService>();

      
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IAMI.IsuzuID.BE.Dealer", Version = "v1" });
            //});






        }

      
    }
}
