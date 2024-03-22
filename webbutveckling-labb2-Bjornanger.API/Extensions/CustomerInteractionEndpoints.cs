using Microsoft.AspNetCore.Mvc;
using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

public static class CustomerInteractionEndpoints
{
    public static IEndpointRouteBuilder MapCustomerInteractionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customer");

        group.MapGet("/order/{Id}", GetOrderFromCustomer);
        group.MapPost("/{Id}", CreateCustomerOrder);
        group.MapPut("/{id}", UpdateCustomerInfo);
        group.MapPatch("/password/{userId}/{newPassword}", UpdateCustomerPassword);

        return app;
    }

   
private static async Task<IResult> GetOrderFromCustomer(IOrderService<Order> orderRepo,ICustomerService<Customer> customerRepo, int userId)
    {
       
        var customerOrderToGet = await customerRepo.GetByIdAsync(userId);
        var customerOrders = customerOrderToGet.Orders.ToList();
        if (customerOrders is null)
        {
            return Results.NotFound("No orders found for this customer");
        }

        return Results.Ok(customerOrders);
    }
    
    private static async Task<IResult> CreateCustomerOrder(IProductService<Product> prodRepo,
        ICustomerService<Customer> customerRepo,IOrderService<Order> orderRepo, OrderDTO request)
    {
        var customer = await customerRepo.GetByIdAsync(request.CustomerId);
        if (customer is null)
        {
            return Results.NotFound($"Customer not found");
        }

        var products = new List<ProductsOrders>();
        foreach (var productOrder in request.ProductOrders)
        {
            var product = await prodRepo.GetByIdAsync(productOrder.ProductId);
            if (product is null)
            {
                return Results.NotFound($"Product not found");
            }
            products.Add(
                new ProductsOrders()
                {
                    Amount = productOrder.Amount, 
                    Product = product
                });
        }

        var order = new Order
        {
            Customer = customer,
            ProductOrders = products,
            OrderDate = DateTime.Now
        };

        await orderRepo.AddAsync(order);

        return Results.Ok("Order Created");
    }

    private static async Task<IResult> UpdateCustomerInfo(ICustomerService<Customer> customerRepo, int id,
        IContactInfoService<ContactInfo> contactRepo, [FromBody] ContactInfo contactInfo)
    {
        var customer = await customerRepo.GetByIdAsync(id);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {id} could not be found");

        }
        var contactInfoId = customer.ContactInfo.Id;


        await contactRepo.UpdateCustomerInfo(contactInfoId, contactInfo);
        return Results.Ok("Contact info are now updated");
    }

    private static async Task<IResult> UpdateCustomerPassword(ICustomerService<CustomerDTO> customerRepo, int userId, string newPassword)
    {
        var customer = await customerRepo.GetByIdAsync(userId);
        if (customer is null)
        {
            return Results.NotFound($"The customer with id {userId} could not be found");
            
        }

        customer.Password = newPassword;
        await customerRepo.UpdateCustomerPasswordAsync(userId, newPassword);
         return Results.Ok("Password updated");
    }
    
}