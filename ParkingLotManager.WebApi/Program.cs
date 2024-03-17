using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParkingLotManager.WebApi;
using ParkingLotManager.WebApi.Data;
using ParkingLotManager.WebApi.Models;
using ParkingLotManager.WebApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


ConfigureAuthentication(builder);
ConfigureSwaggerApi(builder);
ConfigureServices(builder);
ConfigureMvc(builder);

var app = builder.Build();

LoadConfiguration(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


static void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

static void ConfigureSwaggerApi(WebApplicationBuilder builder)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x =>
    {
        x.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Parking Lot Manager API",
            Description = @"A sample ASP.NET Core Web API to manage CRUD operations
            on a Parking Lot Management context and other few things",
            Contact =
            {
                Name = "Matheus Ribeiro",
                Email = "mat.araujoribeiro@gmail.com",
                Url = new Uri("https://github.com/matheusarb")
            },
            License = new OpenApiLicense
            {
                Name = "Mit License"
            },
            Version = "v1"
        });
    });
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDataContext>(options => options.UseSqlServer(connectionString));
    
    builder.Services.AddTransient<TokenService>();
}

static void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

static void LoadConfiguration(WebApplication app)
{
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");
}
