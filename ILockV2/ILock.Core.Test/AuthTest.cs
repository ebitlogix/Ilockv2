// <copyright file="AuthTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ILock.Core.Test
{
    using ILock.Core.Data;
    using ILock.Core.Data.Helpers;
    using ILock.Core.Services.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;
    /// <summary>
    /// The auth test.
    /// </summary>
    public class AuthTest : IClassFixture<DependencyBootstrap>
    {
        private readonly ServiceProvider serviceProvider;
        private readonly ILogger logger;
        private readonly ICryptoService cryptoService;
        private readonly ILoggerFactory loggerFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthTest"/> class.
        /// </summary>
        /// <param name="bootstrap">The bootstrap.</param>
        public AuthTest(DependencyBootstrap bootstrap)
        {
            loggerFactory = Mock.Of<ILoggerFactory>();
            this.serviceProvider = bootstrap.ServiceProvider;
            this.cryptoService = this.serviceProvider.GetRequiredService<ICryptoService>();
            DatabaseScripts databaseScripts = new DatabaseScripts(bootstrap);
        }
        [Theory]
        [InlineData("umer123", "Nrm123")]
        [InlineData("uzair123", "Nrm123")]
        public void Test1(string username, string password)
        {
            var credentials = string.Concat("Basic ", ToBase64(username, password));
            StringValues stringValues = new StringValues(credentials);
            using (var context = serviceProvider.GetService<AuthDBContext>())
            {
                IAuthenticationService authenticationService = serviceProvider.GetService<IAuthenticationService>();
                var res = authenticationService.AuthenticateWithPassword(new Dictionary<string, StringValues> { { "Authorization", stringValues } });
                //Assert.Equal(res.Success, true);
                //Assert.IsType<AuthToken>(res.Token);
            }
        }
        private static string ToBase64(string username, string password)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(($"{username}:{password}")));
        }
        [Fact]
        public void CopyDatabase()
        {
            var targetDbOptions = new DbContextOptionsBuilder<AuthDBContext>()
                    .UseNpgsql("Server=c.uks-mdlru-tst-postgre.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;Password=eL8,cZ5}fJ4+zX0@;Ssl Mode=Prefer;").UseSnakeCaseNamingConvention();

            var targetSecurityContext = new AuthDBContext(targetDbOptions.Options, new DbContextSchema("security"));

            var sourceOptions = new DbContextOptionsBuilder<AuthDBContext>()
                    .UseNpgsql("Server=c.uks-mdlru-dev-postgre.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;Password=eL8,cW5}fP4+zX9#;Ssl Mode=Prefer;").UseSnakeCaseNamingConvention();

            var sourceSecurityContext = new AuthDBContext(sourceOptions.Options, new DbContextSchema("security"));

            //targetSecurityContext.Features.RemoveRange(targetSecurityContext.Features.ToArray());
            //targetSecurityContext.Permissions.RemoveRange(targetSecurityContext.Permissions.ToArray());
            targetSecurityContext.Roles.RemoveRange(targetSecurityContext.Roles.ToArray());
            //targetSecurityContext.Users.RemoveRange(targetSecurityContext.Users.ToArray());
            //targetSecurityContext.FeatureAssociations.RemoveRange(targetSecurityContext.FeatureAssociations.ToArray());

            //targetSecurityContext.Features.AddRange(sourceSecurityContext.Features.ToList());
            //targetSecurityContext.Permissions.AddRange(sourceSecurityContext.Permissions.Include(p => p.Feature).ToList());
            targetSecurityContext.Roles.AddRange(sourceSecurityContext.Roles.Include(r => r.Permissions).ToList());
            //targetSecurityContext.Users.AddRange(sourceSecurityContext.Users.Include(u => u.Roles).ToList());
            //targetSecurityContext.FeatureAssociations.AddRange(sourceSecurityContext.FeatureAssociations.AsNoTracking().ToList());

            targetSecurityContext.SaveChanges();
        }

        [Fact]
        public void UpdatePasswords()
        {

            //var sourceOptions = new DbContextOptionsBuilder<AuthDBContext>()
            //        .UseNpgsql("Server=c.uks-mdlru-tst-postgre.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;Password=eL8,cZ5}fJ4+zX0@;Ssl Mode=Prefer;").UseSnakeCaseNamingConvention();

            //var sourceSecurityContext = new AuthDBContext(sourceOptions.Options, new DbContextSchema("security"));

            //var users = sourceSecurityContext.Users.ToList();
            //users.ForEach(user =>
            //{
            //    user.Password = this.cryptoService.ComputePassword(user.Email, "Nrm123");
            //});

            //sourceSecurityContext.SaveChanges();
        }
    }
}