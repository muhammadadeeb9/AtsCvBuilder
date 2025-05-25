using Microsoft.EntityFrameworkCore;
using AtsCvBuilder.Data;
using AtsCvBuilder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// إضافة خدمات قاعدة البيانات
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

// إضافة Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// إعداد JWT
builder.Services.AddJwtService(builder.Configuration);
// إضافة خدمة CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        // تحديد النطاقات المسموح بها
        policy.WithOrigins(
                "http://localhost:3000",       // مثال: تطبيق React يعمل محليًا
                "https://yourfrontenddomai9999999999999999999999999999n.com" // مثال: تطبيق أمامي على نطاق حقيقي
            )
            .AllowAnyMethod()  // السماح بجميع طرق HTTP (GET, POST, PUT, DELETE, OPTIONS)
            .AllowAnyHeader() // السماح بجميع الرؤوس
            .AllowCredentials();
    });
});
// إضافة التفويض (Authorization)
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    o =>
    {
        o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your JWT  "
        });
        o.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }

        });
    });
var app = builder.Build();
// Configure
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
// تمكين CORS
app.UseCors("AllowSpecificOrigins");
app.MapControllers();
app.Run();