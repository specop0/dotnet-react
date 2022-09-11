using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetReact;

public class Program
{
    public static void Main(string[] args)
    {
        ILogger? logger = null;
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.CustomOperationIds(apiDescription =>
            {
                var operationId = string.Empty;
                if (apiDescription.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
                {
                    operationId = actionDescriptor.MethodInfo.GetCustomAttribute<OperationIdAttribute>()?.Id;
                }

                if (string.IsNullOrEmpty(operationId))
                {
                    logger?.LogError("OperationId missing: {}", apiDescription.ActionDescriptor.DisplayName);
                }

                return operationId;
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();
        logger = app.Logger;

        var pathBase = app.Configuration.GetValue<string>("PathBase");
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.Logger.LogInformation("PathBase: {}", pathBase);
            app.UsePathBase(pathBase);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        // allow cross origin requests (e.g., from frontend in development)
        var allowedCrossOrigin = app.Configuration.GetSection("CORS:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        if (allowedCrossOrigin.Any())
        {
            app.Logger.LogInformation("CORS.AllowedOrigins: {}", string.Join(", ", allowedCrossOrigin));
            app.UseCors(cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowCredentials();
                cors.WithOrigins(allowedCrossOrigin);
            });

            app.Logger.LogInformation("Swagger available at: {}{}", pathBase, "/swagger");
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.DisplayOperationId();
            });
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "api/{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
