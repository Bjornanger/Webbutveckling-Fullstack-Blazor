using DataAccess.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//Kalla p� alla ExtensionsMetoder h�r f�r att f� kontakt
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<CustomerService>();



var app = builder.Build();






await app.RunAsync();
