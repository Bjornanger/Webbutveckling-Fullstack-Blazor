using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

public static class UserEndpointExtensions
{


    
    public static IEndpointRouteBuilder MapUserEndpointExtensions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users");

        group.MapGet("/customers", GetAllCustomers);

        //TODO skapa en egen Admin-lista för att kunna söka och lägga till i.
        group.MapGet("/users/admin", GetAllAdmins);
        group.MapGet("/{userId:int}", GetUserById);
        group.MapGet("/{email}", GetUserByEmail);
        group.MapPost("/customers", AddNewCustomer);
        group.MapPost("/users/admins", AddNewAdmin);
        group.MapDelete("/{userId:int}", DeleteCustomer);
        return app;

    }

    private static async Task<List<Admin>> GetAllAdmins(IAdminService<Admin> adminRepo)
    {
        var allAdmins = await adminRepo.GetAllAsync();

        if (allAdmins is null)
        {
            Results.BadRequest("No Admins found");
            return null;
        }

        Results.Ok();
        return allAdmins.ToList();
    }
    
    private static async Task<IResult> AddNewAdmin(IAdminService<Admin> adminRepo, Admin newAdmin)
    {
        var newAdminToAdd = await adminRepo.GetAllAsync();

        if (newAdminToAdd.ToList().Any(a => a.Id.Equals(newAdmin.Id)))
        {
            return Results.NotFound($"Admin with id:{newAdmin.Id} and {newAdmin.UserName} already exists.");
        }

        
        await adminRepo.AddAsync(newAdmin);
        return Results.Ok("New Admin added.");


    }
    private static async Task<IResult> DeleteCustomer(ICustomerService<Customer> customerRepo, int userId)
    {
        var userToRemove = await customerRepo.GetByIdAsync(userId);
        if (userToRemove is null)
        {
            return Results.BadRequest($"User with {userId}  not found");
            
        }

        
        await customerRepo.DeleteAsync(userToRemove.Id);
        return Results.Ok($"User: {userToRemove.Id} {userToRemove.FirstName} found and Removed.");
    }
    
    private static async Task<IResult> AddNewCustomer(ICustomerService<Customer> customerRepo, CustomerDTO newCustomer)
    {
       

        if (newCustomer is null)
        {
            return null;
        }
        
        var customerToAdd = await customerRepo.GetAllAsync();

        if (customerToAdd.ToList().Any(p => p.Email == newCustomer.Email) || customerToAdd is null)
        {
            return Results.BadRequest($"The Customer with Email: {newCustomer.Email} already exists");
           
        }

        //User user = new User()
        //{
        //    Email = newCustomer.Login.Email,
        //    Password = newCustomer.Login.Password
        //};

        Customer createNewCustomer = new Customer()
        {
            FirstName = newCustomer.FirstName,
            LastName = newCustomer.LastName,
            Email = newCustomer.Email,
            Password = newCustomer.Password,
            ContactInfo = new ContactInfo()
            {
                Phone = newCustomer.ContactInfo.Phone,
                Address = newCustomer.ContactInfo.Address,
                ZipCode = newCustomer.ContactInfo.ZipCode,
                City = newCustomer.ContactInfo.City,
                Region = newCustomer.ContactInfo.Region,
                Country = newCustomer.ContactInfo.Country

            }
        };
        
        await customerRepo.AddAsync(createNewCustomer);
        return Results.Ok("Customer added");

    }

    private static async Task<CustomerDTO?> GetUserByEmail(ICustomerService<Customer> customerRepo, string email)
    {
        var allCustomers = await customerRepo.GetAllAsync();

        var customerEmail = allCustomers.FirstOrDefault(c => c.Email.Equals(email));

        if (customerEmail is null)
        {
            Results.NotFound($"Customer with {customerEmail}does not exist");
        }

        var customerToShow = new CustomerDTO
        {
            Id = customerEmail.Id,
            FirstName = customerEmail.FirstName,
            LastName = customerEmail.LastName,
            Email = customerEmail.Email,
            Password = customerEmail.Password,
            ContactInfo = new ContactInfoDTO
            {
                Phone = customerEmail.ContactInfo.Phone,
                Address = customerEmail.ContactInfo.Address,
                ZipCode = customerEmail.ContactInfo.ZipCode,
                City = customerEmail.ContactInfo.City,
                Region = customerEmail.ContactInfo.Region,
                Country = customerEmail.ContactInfo.Country
            },
            Orders = null,
            Cart = null
        };


        Results.Ok("Email found");
        return customerToShow;
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

    private static async Task<List<CustomerDTO>> GetAllCustomers(ICustomerService<Customer> customerRepo)
    {
        var allCustomers = await customerRepo.GetAllAsync();

        if (allCustomers is null)
        {
            Results.NotFound("No products");
            return null;
        }


        var customerList = allCustomers
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Password = c.Password,
                ContactInfo = new ContactInfoDTO
                {
                    Phone = c.ContactInfo.Phone,
                    Address = c.ContactInfo.Address,
                    ZipCode = c.ContactInfo.ZipCode,
                    City = c.ContactInfo.ZipCode,
                    Region = c.ContactInfo.Region,
                    Country = c.ContactInfo.Country
                },
                Orders = null,
                Cart = null
            }).ToList();


        Results.Ok("Customer do exist.");
        return customerList.ToList();
    }


}