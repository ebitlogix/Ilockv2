using System.Collections.Generic;
using ILock.Core.AspNetCore.Extensions;
using ILock.Core.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddControllers();
services
    .AddILock(
        builder.Configuration,
        options => options.UseSqlServer("Server=.;Database=ILockDb;Trusted_Connection=True;"), "security")
    .RegisterILockControllers();

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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
