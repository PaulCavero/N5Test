using Microsoft.OpenApi.Models;
using N5Test.Broker.Loggings;
using N5Test.Data;
using N5Test.Data.UnitOfWork;
using N5Test.Services.Permissions;
using N5Test.Services.PermissionTypes;
using Serilog;
using N5Test.Util.Configurations;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true).Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<DataBaseConfiguration>(configuration.GetSection("ConnectionStrings"));
builder.Services.AddDbContext<N5testContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPermissionTypeService, PermissionTypeService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddLogging();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        name: "v1",
        info: new OpenApiInfo
        {
            Title = "N5Test",
            Version = "v1"
        }
    );
});

builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
