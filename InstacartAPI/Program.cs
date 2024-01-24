using Instacart_BusinessLogic.BusinessLogics;
using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Instacart_DataAccess.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<DB_context>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
