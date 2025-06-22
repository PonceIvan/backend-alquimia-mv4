using alquimia.Api.Middlewares;
using alquimia.Api.Seed;
using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🗄️ Conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AlquimiaDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging(); // Debugging
});

// 💡 Servicios y dependencias
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IOlfactoryFamilyService, OlfactoryFamilyService>();
builder.Services.AddScoped<IDesignLabelService, DesignLabelService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IMercadoPagoService, MercadoPagoService>();

// 🧩 Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// 🔐 Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AlquimiaDbContext>()
    .AddDefaultTokenProviders();

// 🔐 JWT
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

// 🌐 Google Auth
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

// 📃 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alquimia API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando el esquema Bearer.  
                        Ingresá el token así: Bearer {tu token}.",
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

// 🔓 CORS incluyendo Vercel
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",                          // Next.js dev
            "https://localhost:5035",                         // Swagger
            "https://localhost:5173",                         // Vite dev
            "https://frontend-alquimia.vercel.app"           // ✅ Producción en Vercel
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// ⚙️ Configuración de Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddApplicationInsightsTelemetry();
// 🏁 Build y Middleware
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// 🧪 Seeders
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
    await UserSeeder.SeedAdminAsync(services);
    await ProductSeeder.SeedTiposProductoAsync(services);
    // await UserSeeder.SeedProveedoresAsync(services); // Descomenta si lo necesitás
}

// 📚 Swagger UI
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
