using Hangfire;
using Hangfire.SqlServer;
using Instacart_BusinessLogic.BusinessLogics;
using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Instacart_DataAccess.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddTransient<DB_context>();

builder.Services.AddTransient<Instacart_DataAccess.Service.EmailServices>();

var emailconfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailModel>();

builder.Services.AddSingleton(emailconfig);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IAdminBLL, AdminBLL>();

builder.Services.AddTransient<IPasswordservice,PasswordService>();

builder.Services.AddTransient<IAdminService, AdminService>();

builder.Services.AddTransient<IEmailServices, EmailServices>();

builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<IProductBLL, ProductBLL>();

builder.Services.AddTransient<IAuthservice, AuthServices>();

builder.Services.AddTransient<IAuthBLL, AuthBLL>();

builder.Services.AddTransient<IShopBLL, ShopBLL>();

builder.Services.AddTransient<IShopService, ShopService>();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
                ValidateIssuerSigningKey=true
            };
        });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Instacart API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
            });
});

//builder.Services.AddHangfire(configuration => configuration
//                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//                .UseSimpleAssemblyNameTypeSerializer()
//                .UseRecommendedSerializerSettings()
//                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
//                {
//                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//                    QueuePollInterval = TimeSpan.Zero,
//                    UseRecommendedIsolationLevel = true,
//                    DisableGlobalLocks = true
//                }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHangfireDashboard("/hangfire");

//app.UseHangfireServer();

//RecurringJob.AddOrUpdate<IAuthservice>(n => n.Checktime(), "*/1 * * * * *");

//app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
