using Microsoft.Extensions.DependencyInjection;

namespace ILock.Core.AspNetCore.Extensions
{
    /// <summary>
    /// The extension methods for ILock
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Registers the ilock controllers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection.</returns>
        public static IServiceCollection RegisterILockControllers(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(Extensions).Assembly).AddControllersAsServices();

            return services;
        }
    }
}
