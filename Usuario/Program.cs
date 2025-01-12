using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
var log4netConfigFile = builder.Configuration["Log4NetCore:configFile"];
XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigFile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
