using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

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

    //private static async Task GetAllAdmins(HttpContext context)
    //{

    //}
    //private static async void AddNewAdmin(IAdminService<Admin> newAdmin)
    //{

    //}

    private static async void DeleteCustomer(ICustomerService<Customer> customerRepo, int userId)
    {
        var userToRemove = await customerRepo.GetByIdAsync(userId);
        if (userToRemove is null)
        {
            Results.BadRequest($"User with {userId}  not found");
            return;
        }

        Results.Ok($"User: {userToRemove.Id} {userToRemove.FirstName} found and Removed.");
        await customerRepo.DeleteAsync(userToRemove.Id);
        

    }

   

    private static async void AddNewCustomer(ICustomerService<Customer> customerRepo, Customer newCustomer)
    {
        var customerToAdd = await customerRepo.GetAllAsync();


        if (customerToAdd.ToList().Any(p => p.Id == newCustomer.Id))
        {
            Results.BadRequest($"The Customer with this Email: {newCustomer.Id} already exists");
            return;
        }
        Results.Ok();
        customerRepo.AddAsync(newCustomer);
        
    }

    private static async Task<Customer> GetUserByEmail(ICustomerService<Customer> customerRepo, string email)
    {
        var allCustomers = await customerRepo.GetAllAsync();

        var customerEmail = allCustomers.FirstOrDefault(c => c.Email.Equals(email));

        if (customerEmail is null)
        {
            Results.NotFound($"Customer with {customerEmail}does not exist");
        }

        Results.Ok("Email found");
        return customerEmail;
    }

    private static async Task<Customer> GetUserById(ICustomerService<Customer> customerRepo, int userId)
    {
        var userById = await  customerRepo.GetByIdAsync(userId);
        
        
        if (userById is null)
        {
            Results.BadRequest("User not found");
            return null;
        }

        Results.Ok();
        return userById;
    }
    
    private static async Task<List<Customer>> GetAllCustomers(ICustomerService<Customer> customerRepo)
    {
        var allCustomers = await customerRepo.GetAllAsync();

        if (allCustomers is null)
        {
            return null;
        }

        Results.Ok();
        return allCustomers.ToList();
    }

}