using Microsoft.Extensions.Localization;
using Webbutveckling.BlazorClient.Components;
using Webbutveckling.BlazorClient.Services;
using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("BlazorAPI",
    client =>
        client.BaseAddress = new Uri("https://localhost:7290")
);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IProductService<ProductDTO>, ProductService>();

builder.Services.AddScoped<ICategoryService<CategoryDTO>, CategoryService>();

builder.Services.AddScoped<ICustomerService<CustomerDTO>, CustomerService>();

builder.Services.AddScoped<IContactInfoService<ContactInfoDTO>, ContactInfoService>();

builder.Services.AddScoped<IOrderService<OrderDTO>, OrderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
