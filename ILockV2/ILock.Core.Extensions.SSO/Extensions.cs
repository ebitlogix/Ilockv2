using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ILock.Core.Extensions.AspNetCore.Mvc.Saml2
{
    public static class Extensions
    {
        /// <summary>
        /// Registers the SSO controllers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection RegisterSSOControllers(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Extensions).Assembly).AddControllersAsServices();
            return services;
        }
    }
}