using DataAccess.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//Kalla på alla ExtensionsMetoder här för att få kontakt
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<CustomerService>();



var app = builder.Build();






await app.RunAsync();
