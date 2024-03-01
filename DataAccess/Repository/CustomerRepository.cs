using DataAccess.Entities;

namespace DataAccess.Repository;

public class CustomerRepository
{
    public List<Customer> Customers { get; set; } = new()
    {
        new Customer
        {
            Id = 1,
           Email = "BlazorMan@ASP.com",
            Password = "password",
            FirstName = "Sune",
            LastName = "Thorleifsson",
            ContactInfo = new ContactInfo
            {
                Phone = "0700-668909",
                Address = "SuneVägen 9",
                ZipCode = "01337",
                City = "Borås",
                Region = "Västra Götaland",
                Country = "Sverige"
            },
            
        },
        new Customer
        {
            Id = 2,
            Email = "hej@då.com",
            Password = "hej",
            FirstName = "Håkan",
            LastName = "Bråkan",
            ContactInfo = new ContactInfo
            {
                Phone = "071-3671946",
                Address = "Ankeborgsgatan 3",
                ZipCode = "L00000L",
                City = "Ankeborg",
                Region = "Anklän",
                Country = "Ankh"
            }
        },
        new Customer
        {
            Id = 3,
            FirstName = "John",
            LastName = "Doe",
            ContactInfo = new ContactInfo
            {
                Phone = "073 - 123 45 67",
                Address = "Herkulesgatan 11",
                ZipCode = "417 04",
                City = "Göteborg",
                Country = "Sverige"
            },
            Email = " this@that.com",
            Password = "password",
        }
    };
}