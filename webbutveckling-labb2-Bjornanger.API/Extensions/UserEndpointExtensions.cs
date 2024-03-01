using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlazorLABB.Client.Extensions;

public static class UserEndpointExtensions
{


    //TODO Skapa en Context-länk för att kunna spara till DB
    public static IEndpointRouteBuilder MapUserEndpointExtensions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users");

        group.MapGet("/customers", GetAllCustomers);

        //TODO skapa en egen Admin-lista för att kunna söka och lägga till i.
        //group.MapGet("/users/admin", GetAllAdmins);
        group.MapGet("/{userId:int}", GetUserById);
        group.MapGet("/{email}", GetUserByEmail);

        group.MapPost("/customers", AddNewCustomer);
        //group.MapPost("/users/admins", AddNewAdmin);
        group.MapDelete("/{userId:int}", DeleteCustomer);
        return app;

    }

    private static void DeleteCustomer(CustomerRepository service, int userId)
    {
        var userToRemove = service.Customers.FirstOrDefault(u => u.Id == userId);
        if (userToRemove is null)
        {
            Results.BadRequest($"User with {userId}  not found");
            return;
        }

        Results.Ok($"User: {userToRemove.Id} {userToRemove.FirstName} found and Removed.");
        service.Customers.Remove(userToRemove);
        //context.SaveChanges();
        
    }

    //private static void AddNewAdmin(CustomerService service, Admin admin)
    //{
       
    //}

    private static void AddNewCustomer(CustomerRepository service, Customer customer)
    {
        if (service.Customers.Any(p => p.Email == customer.Email))
        {
            Results.BadRequest($"The Customer with this Email: {customer.Email} already exists");
            return;
        }

        Results.Ok();
        service.Customers.Add(customer);
        //context.SaveChanges();

    }

    private static Customer GetUserByEmail(CustomerRepository service, string email)
    {
        var customerEmail = service.Customers.Find(c => c.Email == email);
        if (customerEmail is null)
        {
            Results.BadRequest("Email not Found");
            return null;
        }

        Results.Ok("Email found");
        return customerEmail;
    }

    private static Customer GetUserById(CustomerRepository service, int userId)
    {
        var userById = service.Customers.FirstOrDefault(i => i.Id == userId);

        if (userById is null)
        {
            Results.BadRequest("User not found");
            return null;
        }

        Results.Ok();
        return userById;

    }

    //private static Task GetAllAdmins(HttpContext context)
    //{
    //    throw new NotImplementedException();
    //}

    private static List<Customer> GetAllCustomers(CustomerRepository service)
    {
        var allCustomers = service.Customers;

        if (allCustomers is null)
        {
            return null;
        }

        Results.Ok();
        return allCustomers.ToList();
    }

}