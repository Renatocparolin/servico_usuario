using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Usuario", Version = "v1" }); });

// Configure log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
var log4netConfigFile = builder.Configuration["Log4NetCore:configFile"];
XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigFile));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
