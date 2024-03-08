using DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using webbutveckling_labb2_Bjornanger.API.Extensions;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("WebLabbGroceryStoreDB");

builder.Services.AddDbContext<GroceryStoreDbContext>(
    options => options.UseSqlServer(connectionString)
    );


builder.Services.AddScoped<IAdminService<Admin>, AdminRepository>();
builder.Services.AddScoped<ICategoryService<Category>, CategoryRepository>();
builder.Services.AddScoped<ICustomerService<Customer>, CustomerRepository>();
builder.Services.AddScoped<IContactInfoService<ContactInfo>, ContactInfoRepository>();
builder.Services.AddScoped<IOrderService<Order>, OrderRepository>();
builder.Services.AddScoped<IProductService<Product>, ProductRepository>();





var app = builder.Build();



app.MapProductEndpointExtensions();
app.MapCategoryEndpoints();
app.MapCustomerInteractionEndpoints();
app.MapUserEndpointExtensions();

app.Run();

