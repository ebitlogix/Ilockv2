using System.Text;
using ILock.Core.Data;
using ILock.Core.Data.Helpers;
using ILock.Core.Services.Abstractions;
using ILock.Core.Services.Implmentations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ILock.Core.Services.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Authentication, Configuration, DbcontextFactory and Services
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="configuration">configuration</param>
        /// <param name="configureDbSource">configureDbSource</param>
        /// <param name="defaultSchema">schema</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddILock(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> configureDbSource,
            string defaultSchema = null)
        {
            Configuration config = new Configuration(configuration);
            services.AddSingleton(config);

            var securtityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.TokenSettings.Key));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters =
                                new TokenValidationParameters
                                {
                                    ValidIssuer = config.TokenSettings.Issuer,
                                    ValidAudience = config.TokenSettings.Audience,
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = securtityKey
                                };
                        });

            services.AddSingleton<IDbContextSchema>(new DbContextSchema(defaultSchema));

            services.AddDbContextFactory<AuthDBContext>(configureDbSource, ServiceLifetime.Scoped);
            services.AddScoped<AuthDBContext>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISSOService, SSOService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IPolicyService, PolicyService>();

            // CryptoService
            services.AddScoped<ICryptoService, CryptoService>();

            // Authentication Service
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Creating Singleton Service Factory to get Services when needed
            DependencyInjectionServiceFactory.Init(services.BuildServiceProvider());

            return services;
        }
    }
}