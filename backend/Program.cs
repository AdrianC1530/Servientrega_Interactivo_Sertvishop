using Microsoft.EntityFrameworkCore;
using Servientrega.Api.Data;
using Servientrega.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS for Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configure DbContext with MySQL
var connectionString = builder.Configuration.GetConnectionString("GuardiasDB");
builder.Services.AddDbContext<ServientregaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register custom services
builder.Services.AddScoped<RoutingService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
