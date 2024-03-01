using BlazorLABB.Client.Extensions;
using DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ProductRepository>();//TODO ändra dess till Scooped
builder.Services.AddSingleton<CustomerRepository>();//TODO ändra dess till Scooped
builder.Services.AddSingleton<CategoryRepository>();//TODO ändra dess till Scooped






var app = builder.Build();


app.MapProductEndpointExtensions();
app.MapCategoryEndpoints();


app.MapCustomerInteractionEndpoints();
app.MapUserEndpointExtensions();

app.Run();

