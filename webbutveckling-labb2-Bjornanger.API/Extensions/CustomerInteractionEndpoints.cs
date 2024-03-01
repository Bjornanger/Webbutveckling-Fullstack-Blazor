using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

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

    private static List<Product?> GetAllItemsFromCustomerCart(CustomerRepository service, int userId)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.BadRequest($"The customer with id {userId} could not be found");
            return null;
        }

        var items = customer.Cart.ToList();

        Results.Ok();
        return items;

    }

    private static Order CreateCustomerOrder(CustomerRepository service, int userId)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer?.Cart.Count == 0)
        {
            Results.BadRequest("The cart is empty");
            return null;
        }

        //TODO Se över
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
        Results.Ok();
        return order;
    }

    private static void UpdateCustomerInfo(CustomerRepository service, int userId, [FromBody] ContactInfo contactInfo)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.ContactInfo = contactInfo;
        Results.Ok();
    }
    private static void UpdateCustomerPassword(CustomerRepository service, int userId, string newPassword)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.Password = newPassword;
        Results.Ok();
    }

    private static void ClearCustomerCart(CustomerRepository service, int userId)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        customer.Cart.Clear();
        Results.Ok();
    }

    private static void AddProductToCustomerCart(CustomerRepository service, ProductRepository prodService, int userId, int productId)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        var product = prodService.Products.FirstOrDefault(p => p.Id == productId);

        customer.Cart.Add(product);
        Results.Ok();
    }

    private static void RemoveProductFromCustomerCart(CustomerRepository service, ProductRepository pService, int userId, int productId)
    {
        var customer = service.Customers.FirstOrDefault(c => c.Id == userId);
        if (customer is null)
        {
            Results.NotFound($"The customer with id {userId} could not be found");
            return;
        }

        var product = pService.Products.FirstOrDefault(p => p.Id == productId);

        customer.Cart.Remove(product);
        Results.Ok();
    }
}