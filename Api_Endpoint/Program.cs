using Application;
using Infrastructure;
using log4net.Config;
using Logging;

var builder = WebApplication.CreateBuilder(args);
//Configure Log4net.
XmlConfigurator.Configure(new FileInfo("log4net.config"));

// Add services to the container.
builder.Services.AddControllers();

// Add Application Layer IOC
builder.Services.AddApplicationLayer();
// Add Infrastructure Layer IOC
builder.Services.AddInfrastructureLayerServices(builder.Configuration);
// Add Logging Layer IOC
builder.Services.AddLoggingLayerServices();

// Api Versioning
builder.Services.AddApiVersioning();
// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHealthChecks();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
