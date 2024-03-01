using DataAccess.Entities;

namespace DataAccess.Repository;

public class ProductRepository
{
    public List<Product> Products { get; set; } = new()
    {
        new Product
        {
            Id = 1,
            Name = "Squid",
            Description = "Squishy and yummy yummy",
            Price = 35,
            Category = new Category
            {
                
                Name = "Seafood",
                
            },
            ImageUrl = "../image",
            Stock = 7
        },
        new Product
        {
            Id = 2,
            Name = "Tuna",
            Description = "Oceans racecar in a bag",
            Price = 39,
            Category = new Category
            {

                Name = "Seafood",

            },
            ImageUrl = "../image",
            Stock = 10
        },
        new Product
        {
            Id = 3,
            Name = "Broccoli",
            Description = "Green is gooood",
            Price = 35,
            Category = new Category
            {

                Name = "Vegetables",

            },
            ImageUrl = "../image",
            Stock = 9
        },
        new Product
        {
            Id = 4,
            Name = "Butter",
            Description = "I can't believe it's butter",
            Price = 49,
            Category = new ()
            {
                Name = "Dairy",
            },
            ImageUrl = "../image",
            Stock = 2
        }, new Product
        {
            Id = 5,
            Name = "Baseball",
            Description = "I can't believe it's baseball",
            Price = 89,
            Category = new ()
            {
                Name = "Sporting goods",
            },
            ImageUrl = "../image",
            Stock = 5
        }, new Product
        {
            Id = 6,
            Name = "Kaplastavar",
            Description = "Nice",
            Price = 109,
            Category = new ()
            {
                Name = "Sporting goods",
            },
            ImageUrl = "../image",
            Stock = 10
        },
        new()
        {
            Id = 7,
            Name = "Ganska stor sten",
            Description = "Mja, halvstor åtminstone",
            Price = 9000,
            Category = new Category
            {
                Name = "Stenar",
            },
            Status = false,
            ImageUrl = "https://gillgrens.se/wp-content/uploads/2015/05/Sten-Mellan-160x270mm-40158.jpg",
            Stock = 9999
        },
        new()
        {
            Id = 8,
            Name = "Flamingo",
            Description = "Jävligt fin",
            Price = 56,
            Category = new Category
            {
                Name = "Plastdjur",
            },
            Status = false,
            ImageUrl = "https://invention.si.edu/sites/default/files/blog-maher-meg-2020-08-21-original-flamingos-450-inline-edit.jpg",
            Stock = 79
        },
        new()
        {
            Id = 9,
            Name = "Krokofant",
            Description = "Kryptozoologiskt under",
            Price = 1000000,
            Category = new Category()
            {
                Name = "Kryptozoologi"
            },
            Status = false,
            ImageUrl = "https://livspusselhome.files.wordpress.com/2013/08/0d96b-fabeldjur.jpg",
            Stock = 1
        }
    };
}