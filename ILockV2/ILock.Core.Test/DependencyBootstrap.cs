using ILock.Core.Data;
using ILock.Core.Services.Abstractions;
using ILock.Core.Services.Implmentations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ILock.Core.Test
{
    public class DependencyBootstrap
    {
        public DependencyBootstrap()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new ConfigurationBuilder().SetBasePath("D:/Retail Insight - RI/POCs/research-development/ILockV2/ILock.Core.Test").AddJsonFile("testsettings.json");
            var configuration = builder.Build();
            // DbContext
            serviceCollection
                .AddDbContextFactory<AuthDBContext>(options => options.UseNpgsql("Server=localhost;Database=ILockDb;Port=5432;User Id=postgres;Password=Umer12#$;Ssl Mode=Disable;").UseSnakeCaseNamingConvention()
        ,
                    ServiceLifetime.Transient);

            // TokenSettings
            serviceCollection.AddSingleton(_ => new TokenSettings()
            {
                Audience = "",
                Issuer = "",
                ExpiresIn = 4800,
                Key = "NRM@#$%123The encryption algorithm 'System.String' requires a key size of at least 'System.Int32' bits",
            });
            serviceCollection.AddSingleton(_ => new Configuration(configuration));

            // Authentication Service
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();

            // CryptoService
            serviceCollection.AddScoped<ICryptoService, CryptoService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class ILockDbContextFactory : IDesignTimeDbContextFactory<AuthDBContext>
    {
        public AuthDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDBContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Database=ILockDb;Port=5432;User Id=postgres;Password=Umer12#$;Ssl Mode=Disable;");
            var bootstrap = new DependencyBootstrap();

            var serviceProvider = bootstrap.ServiceProvider;
            var iLoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            return new AuthDBContext(optionsBuilder.Options);
        }
    }
}
