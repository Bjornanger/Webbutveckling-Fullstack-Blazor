using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace BlazorLABB.Client.Extensions;

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

        Results.Ok();
        return items;

    }

    private static async Task<Order> CreateCustomerOrder(ICustomerService<Customer> customerRepo, int userId)
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
            OrderDate = DateTime.Now,
            TotalPrice = totalPrice,
            Status = false,
        };


        //TODO Lägg till att rensa cart
        Results.Ok("Customer cart are now added to a order");
        return order;
        
    }

    private static async void UpdateCustomerInfo(ICustomerService<Customer> customerRepo, int userId, [FromBody] ContactInfo contactInfo)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.ContactInfo = contactInfo;
        Results.Ok("Contact info are now updated");
    }

    private static async void UpdateCustomerPassword(ICustomerService<Customer> customerRepo, int userId, string newPassword)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.Password = newPassword;
        Results.Ok("Password updated");
    }

    private static async void ClearCustomerCart(ICustomerService<Customer> customerRepo, int userId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.Cart.Clear();
        Results.Ok("Customer cart are now empty.");
    }

    private static async void AddProductToCustomerCart(ICustomerService<Customer> customerRepo, IProductService<Product> productRepo, int userId, int productId)
    {
        var customer =  await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        var product = await productRepo.GetByIdAsync(productId);

        customer.Cart.Add(product);
        Results.Ok();
    }

    private static async void RemoveProductFromCustomerCart(ICustomerService<Customer> customerRepo, IProductService<Product> productRepo, int userId, int productId)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        var product = await productRepo.GetByIdAsync(productId);

        customer.Cart.Remove(product);
        Results.Ok();
    }
}