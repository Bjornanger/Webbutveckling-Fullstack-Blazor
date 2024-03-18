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

builder.Services.AddScoped<IProductService<ProductDTO>, ProductService>();//Endast för att hantera produkterna i affären

builder.Services.AddScoped<ICategoryService<CategoryDTO>, CategoryService>();//Endast för att hantera kategorier i affären

builder.Services.AddScoped<ICustomerService<CustomerDTO>, CustomerService>();

builder.Services.AddScoped<IContactInfoService<ContactInfoDTO>, ContactInfoService>();

//builder.Services.AddScoped<ICustomerService<UserDTO>, UserService>();


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
