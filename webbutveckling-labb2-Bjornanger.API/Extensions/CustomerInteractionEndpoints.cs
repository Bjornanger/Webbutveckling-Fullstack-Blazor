using Microsoft.AspNetCore.Mvc;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

public static class CustomerInteractionEndpoints
{
    public static IEndpointRouteBuilder MapCustomerInteractionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customer");

        group.MapGet("/{userId}", GetAllItemsFromCustomerCart);
        group.MapPost("/{userId}", CreateCustomerOrder);
        group.MapPatch("/{userId}", UpdateCustomerInfo);
        group.MapPatch("/password/{userId}/{newPassword}", UpdateCustomerPassword);
        group.MapPatch("/cart/{userId}", ClearCustomerCart);
        group.MapPut("/cart/{userId}/{productId}", AddProductToCustomerCart);
        group.MapDelete("/cart/{userId}/{productId}", RemoveProductFromCustomerCart);

        return app;
    }

    private static async Task<List<Product?>> GetAllItemsFromCustomerCart(ICustomerService<Customer> customerRepo, int userId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.BadRequest($"The customer with id {userId} could not be found");
            return null;
        }

        var items = customer.Cart.ToList();

        foreach (var prod in items)
        {
            if (prod is null)
            {
                Results.NotFound("No products found in cart");
                return null;
            }
            
            
                Console.WriteLine($"Product: {prod.Name}");
                Console.WriteLine($"Description: {prod.Description}");
                Console.WriteLine($"Price: {prod.Price}");
            

            
        }


        Results.Ok();
        return items;

    }

    private static async Task<Order> CreateCustomerOrder(ICustomerService<Customer> customerRepo,IOrderService<Order> orderRepo, int userId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);

        if (customer is null)
        {
              Results.BadRequest($"The customer with id {userId} could not be found");
              return null;
        }

        if (customer?.Cart.Count == 0)
        {
            Results.BadRequest("The cart is empty");
            return null;
        }

        //TODO Lägg till att räkna ut totalpris till FRONTEND 
        var totalPrice = customer.Cart.Sum(p => p.Price);

        var order = new Order
        {
            CustomerId = customer.Id,
            ProductsInOrder = customer.Cart,
            OrderDate = DateTime.UtcNow,
            TotalPrice = totalPrice,
            Status = false,
        };

        await orderRepo.AddAsync(order);
       
        var allOrders = await orderRepo.GetAllAsync();

        if (allOrders is null)
        {
             Results.NotFound("No orders found");
             return null;
        }

        var lastOrder = allOrders.ToList().LastOrDefault();
        
        foreach (var product in customer.Cart.DistinctBy(p => p.Id))
        {
            var lastOrderId = lastOrder.Id;
            var productId = product.Id;
            var productAmount = customer.Cart.Select(p => p.Id == product.Id).Count();
            await orderRepo.AddToProductOrders(new ProductsOrders
            {
                ProductId = productId,
                OrderId = lastOrderId,
                Amount = productAmount,
                
            });
            
        }
        customer.Orders.Add(lastOrder.Id);
        
        
        

        //TODO Hämta Metoden ClearCustomerCart och använd den här för att tömma kundvagnen efter att ordern är skapad.
        
        
        return order;
        

    }

    private static async Task<IResult> UpdateCustomerInfo(ICustomerService<Customer> customerRepo, int userId, [FromBody] ContactInfo contactInfo)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        customer.ContactInfo = contactInfo;
         return Results.Ok("Contact info are now updated");
    }

    private static async Task<IResult> UpdateCustomerPassword(ICustomerService<Customer> customerRepo, int userId, string newPassword)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        customer.Password = newPassword;
         return Results.Ok("Password updated");
    }

    private static async Task<IResult> ClearCustomerCart(ICustomerService<Customer> customerRepo, int userId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        customer.Cart.Clear();
         return Results.Ok("Customer cart are now empty.");
    }

    private static async Task<IResult> AddProductToCustomerCart(ICustomerService<Customer> customerRepo, IProductService<Product> productRepo, int userId, int productId)
    {
        var customer =  await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        var product = await productRepo.GetByIdAsync(productId);

        customer.Cart.Add(product);
        return Results.Ok();
    }

    private static async Task<IResult> RemoveProductFromCustomerCart(ICustomerService<Customer> customerRepo, IProductService<Product> productRepo, int userId, int productId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        var product = await productRepo.GetByIdAsync(productId);

        customer.Cart.Remove(product);
         return Results.Ok();
    }



}