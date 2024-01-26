using AspNetCoreRateLimit;
using FinancialDocumentRetrieval.Api.Middleware;
using FinancialDocumentRetrieval.DAL;
using FinancialDocumentRetrieval.Models.Common.Config;
using Serilog;

namespace FinancialDocumentRetrieval.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        builder.Services.AddCors();
        ConfigProvider.Setup(builder.Configuration);

        // Avoiding DOS attacks with Rate Limit
        builder.Services.ConfigureRateLimit(builder.Configuration);

        // Global Http Cache Headers Configure, if it is needed can be specified also on action/controller level
        builder.Services.ConfigureHttpCacheHeaders();

        builder.Services.AddBL();
        builder.Services.AddDataAccess(builder.Configuration);

        builder.Services.AddJwt(builder.Configuration);

        // TODO If a new version of API is required, and we have to keep current we can that achieve by API versioning
        builder.Services.ConfigureApiVersioning();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        var app = builder.Build();
        // Configure the HTTP request pipeline.

        app.UseSerilogRequestLogging();

        app.UseMiddleware<ExceptionMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder =>
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:7261"));

        app.UseResponseCaching();
        app.UseHttpCacheHeaders();

        app.UseIpRateLimiting();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}