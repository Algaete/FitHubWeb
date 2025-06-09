using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "FitHub API",
        Version = "v1",
        Description = "API para el sistema FitHub"
    });

    // Configuración de seguridad para Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de Supabase
builder.Services.AddSingleton<Supabase.Client>(sp =>
{
    var url = "https://mppqnucxvzegmzifptbf.supabase.co";
    var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1wcHFudWN4dnplZ216aWZwdGJmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDc4ODAxMjIsImV4cCI6MjA2MzQ1NjEyMn0.Loo8gwqtcdIiicQfgVlnTSn-vPfBJlT6fc8pw27eM_I";
    
    var options = new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    };
    
    return new Supabase.Client(url, key, options);
});

// Registro de repositorios
builder.Services.AddScoped<CoreMain.Interfaces.IGymRepository, CoreMain.Repositories.Implementations.GymRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IUserRepository, CoreMain.Repositories.Implementations.UserRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IPlanRepository, CoreMain.Repositories.Implementations.PlanRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IClassRepository, CoreMain.Repositories.Implementations.ClassRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IClassScheduleRepository, CoreMain.Repositories.Implementations.ClassScheduleRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IInstructorRepository, CoreMain.Repositories.Implementations.InstructorRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IInstructorGymRepository, CoreMain.Repositories.Implementations.InstructorGymRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IUserPlanRepository, CoreMain.Repositories.Implementations.UserPlanRepository>();
builder.Services.AddScoped<CoreMain.Interfaces.IClassBookingRepository, CoreMain.Repositories.Implementations.ClassBookingRepository>();

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitHub API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configuración de CORS y HTTPS
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
