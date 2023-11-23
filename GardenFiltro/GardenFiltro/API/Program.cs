using System.Reflection;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
//builder.Services.AddJwt(builder.Configuration);
//builder.Services.ConfigureRateLimiting();
//builder.Services.ConfigureApiVersioning();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.AddDbContext<DbAppContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
       using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<DbAppContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var _logger = loggerFactory.CreateLogger<Program>();
                _logger.LogError(ex, "Failed to migrate");
            }
        }




// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
//app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// dotnet ef dbcontext scaffold "server=localhost;user=root;password=123456;database=gardensdb" Pomelo.EntityFrameworkCore.MySql -s ./API/ -p ./Persistence/ --context DbAppContext --context-dir Data --output-dir Entities
// dotnet ef migrations add FirstMig --project .\Persistence\ --startup-project .\API\ --output-dir ./Data/Migrations
// dotnet ef database update --project .\Persistence --startup-project .\API\