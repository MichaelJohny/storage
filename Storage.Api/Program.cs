using Serilog;
using Storage.Api;

var builder = WebApplication.CreateBuilder(args);
var seqServerUrl = builder.Configuration["seq:Url"];
Log.Logger = LoggingBuilder.BuildLogging(seqServerUrl);
Log.Logger.Information("-- Hello Storage Service --");
builder.Host.UseSerilog();

// Add services to the container.
var services = builder.Services;
services.AddServices(builder);

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();