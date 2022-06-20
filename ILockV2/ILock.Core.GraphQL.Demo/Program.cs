//< copyright file = "Program.cs" company = "PlaceholderCompany" >
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using GraphQL.Server.Ui.Voyager;
using ILock.Core.AspNetCore.Extensions;
using ILock.Core.Extensions.Data;
using ILock.Core.Extensions.AspNetCore.Mvc.Saml2;
using ILock.Core.GraphQL.Demo.Data;
using ILock.Core.GraphQL.Demo.Data.Entities;
using ILock.Core.GraphQL.Demo.Mutations;
using ILock.Core.GraphQL.Demo.Queries;
using ILock.Core.GraphQL.Demo.QueryExtensions;
using ILock.Core.GraphQL.Extensions;
using ILock.Core.Services.Extensions;
using ITfoxtec.Identity.Saml2;
using ITfoxtec.Identity.Saml2.MvcCore;
using ITfoxtec.Identity.Saml2.MvcCore.Configuration;
using ITfoxtec.Identity.Saml2.Schemas.Metadata;
using ITfoxtec.Identity.Saml2.Util;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var appEnvironment = builder.Environment;
// Configuring SAML2
services.Configure<Saml2Configuration>(builder.Configuration.GetSection("Saml2"));
services.Configure<Saml2Configuration>(saml2Configuration =>
{
    saml2Configuration.SigningCertificate = CertificateUtil.Load(appEnvironment.MapToPhysicalFilePath(builder.Configuration["Saml2:SigningCertificateFile"]), builder.Configuration["Saml2:SigningCertificatePassword"]);
    saml2Configuration.SignatureValidationCertificates.Add(CertificateUtil.Load(appEnvironment.MapToPhysicalFilePath(builder.Configuration["Saml2:SigningValidationCertificateFile"])));
    saml2Configuration.AllowedAudienceUris.Add(saml2Configuration.Issuer);
    saml2Configuration.AllowedAudienceUris.Add(builder.Configuration["Saml2:Audience"]);
    var entityDescriptor = new EntityDescriptor();
    entityDescriptor.ReadIdPSsoDescriptorFromFile(appEnvironment.MapToPhysicalFilePath(builder.Configuration["Saml2:IdPMetadata"]));
    if (entityDescriptor.IdPSsoDescriptor != null)
    {
        saml2Configuration.SingleSignOnDestination = entityDescriptor.IdPSsoDescriptor.SingleSignOnServices.First().Location;
        //saml2Configuration.SingleLogoutDestination = entityDescriptor.IdPSsoDescriptor.SingleLogoutServices.First().Location; saml2Configuration.SignatureValidationCertificates.AddRange(entityDescriptor.IdPSsoDescriptor.SigningCertificates);
    }
    else { throw new InvalidOperationException("IdPSsoDescriptor not loaded from metadata."); }
});
services.AddSaml2(slidingExpiration: true);
// Add services to the container.
services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

services.AddResponseCompression();

// Adding ILock
services
    .AddILock(
        builder.Configuration,
        options => options.UseNpgsql("Server=c.uks-mdlru-tst-postgre.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;Password=eL8,cZ5}fJ4+zX0@;Ssl Mode=Prefer;").UseSnakeCaseNamingConvention(),
        "security")
    .CreateILockTables()
    .RegisterILockControllers()
    .RegisterSSOControllers();
// Add DbContext
services.AddDbContext<DemoDbContext>(options => options.UseSqlServer("Server=.;Database=ILockDb;Trusted_Connection=True;"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
var corsUrls = builder.Configuration.GetSection("AllowedDomains:Urls").Get<List<string>>();
services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.WithOrigins(corsUrls.ToArray())
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
services.AddGraphQLServer()
                .AddGQLAuthorization()
                .AddILockAuthQueries()
                .AddILockAuthMutationTypes()
                .AddTypeExtension<EventMutation>()
                .AddTypeExtension<UserRoleMutation>()
                .AddTypeExtension<EventQueryExtension>()
                .AddTypeExtension<UserAssociationQueryResolver>()
                .AddTypeExtension<CountryQueryResolver>()
                .AddTypeExtension<RetailerQueryResolver>()
                .AddTypeExtension<ScenarioQueryResolver>()
                .AddType<EventEntity>()
                .AddFiltering()
                .AddSorting();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("Cors");
app
    .UseRouting().UseAuthentication().UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });
app.UseGraphQLVoyager(
    new VoyagerOptions()
    {
        GraphQLEndPoint = "/graphql"
    }, "/graphql-ui");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
app.MapControllers();
app.Run();