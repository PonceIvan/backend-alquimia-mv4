using alquimia.Data.Data.Entities;
using alquimia.Services.Services;
using alquimia.Services.Services.Interfaces;
using backendAlquimia.alquimia.Services;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Base de datos
var connectionString = Environment.GetEnvironmentVariable("ALQUIMIA_DB_CONNECTION")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AlquimiaDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🧠 Servicios de dominio
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IQuizService, QuizService>();

// 🧩 Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AlquimiaDbContext>()
    .AddDefaultTokenProviders();

// 🔐 JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 🌐 Google Authentication (opcional)
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["OAuth:ClientID"];
        options.ClientSecret = builder.Configuration["OAuth:ClientSecret"];
        options.CallbackPath = "/signin-google";
        options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
    });

// 🍪 Cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// 🔄 Controladores y JSON
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// 🔓 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",    // Next.js dev server
            "https://localhost:5035",   // Swagger
            "https://localhost:5173"    // Vite auth
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// 📘 Swagger + JWT Support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Alquimia API",
        Version = "v1",
    });

    // ✅ Soporte para el botón "Authorize"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ej: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// 🌐 Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alquimia API V1");
    c.RoutePrefix = string.Empty;
});

app.UseStaticFiles();
app.UseCors("FrontendPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();