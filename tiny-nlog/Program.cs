using NLog;
using NLog.Web;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;
using tiny;

var builder = WebApplication.CreateBuilder(args);
//var logger = LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config")).GetCurrentClassLogger();
// Add services to the container.
var logger = LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config")).GetCurrentClassLogger();
logger.Info("Testing");
try
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<ILoggerManager, LoggerManager>();
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.Services.GetRequiredService<ILoggerManager>();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}

