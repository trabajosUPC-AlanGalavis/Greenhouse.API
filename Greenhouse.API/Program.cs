using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Persistence.Repositories;
using Greenhouse.API.Crops.Services;
using Greenhouse.API.Profiles.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Services;
using Greenhouse.API.Profiles.Persistence.Repositories;
using Greenhouse.API.Profiles.Services;
using Greenhouse.API.Security.Authorization.Handlers.Implementations;
using Greenhouse.API.Security.Authorization.Handlers.Interfaces;
using Greenhouse.API.Security.Authorization.Middleware;
using Greenhouse.API.Security.Authorization.Settings;
using Greenhouse.API.Security.Domain.Repositories;
using Greenhouse.API.Security.Domain.Services;
using Greenhouse.API.Security.Persistence.Repositories;
using Greenhouse.API.Security.Services;
using Greenhouse.API.Shared.Domain.Repositories;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add Api Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Greenhouse API",
        Version = "v1",
        Description = "Greenhouse RESTful API",
        Contact = new OpenApiContact
        {
            Name = "Greenhouse Platform",
            Url = new Uri("https://upc-pre-202302-si730-sw51-integradis.github.io/LandingPage/")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type =
                    ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Dependency Injection Configuration

// Crop
builder.Services.AddScoped<ICropRepository,CropRepository>();
builder.Services.AddScoped<ICropService, CropService>();
// Formula
builder.Services.AddScoped<IFormulaRepository,FormulaRepository>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
// Preparation Area
builder.Services.AddScoped<IPreparationAreaRepository,PreparationAreaRepository>();
builder.Services.AddScoped<IPreparationAreaService, PreparationAreaService>();
// Bunker
builder.Services.AddScoped<IBunkerRepository,BunkerRepository>();
builder.Services.AddScoped<IBunkerService, BunkerService>();
// Tunnel
builder.Services.AddScoped<ITunnelRepository,TunnelRepository>();
builder.Services.AddScoped<ITunnelService, TunnelService>();
// Grow Room Record
builder.Services.AddScoped<IGrowRoomRecordRepository, GrowRoomRecordRepository>();
builder.Services.AddScoped<IGrowRoomRecordService, GrowRoomRecordService>();

// Company
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
// Employee
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(Greenhouse.API.Crops.Mapping.ModelToResourceProfile),
    typeof(Greenhouse.API.Crops.Mapping.ResourceToModelProfile),
    typeof(Greenhouse.API.Profiles.Mapping.ModelToResourceProfile),
    typeof(Greenhouse.API.Profiles.Mapping.ResourceToModelProfile),
    typeof(Greenhouse.API.Security.Mapping.ModelToResourceProfile),
    typeof(Greenhouse.API.Security.Mapping.ResourceToModelProfile)
    );

var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();