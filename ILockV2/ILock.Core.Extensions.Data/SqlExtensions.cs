using ILock.Core.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace ILock.Core.Extensions.Data
{
    /// <summary>
    /// The SQL Data Extensions.
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Creates ILock tables if not already created
        /// </summary>
        /// <param name="services">services</param>
        /// <returns>IServiceCollection</returns>
        /// <exception cref="DbException"></exception>
        public static IServiceCollection CreateILockTables(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<AuthDBContext>();

            // Creating ILock tables
            using (var dbContext = serviceProvider.GetRequiredService<AuthDBContext>())
            {
                RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();
                try
                {
                    databaseCreator.CreateTables();
                    logger.Log(LogLevel.Information, $"ILock Tables created successfully with given schema");
                }
                catch (DbException exception)
                {
                    logger.Log(LogLevel.Information, $"{exception.Message} ILock Tables already exist with given schema");
                }
            }

            return services;
        }
    }
}