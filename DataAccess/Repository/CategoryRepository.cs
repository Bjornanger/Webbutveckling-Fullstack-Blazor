using DataAccess.Entities;

namespace DataAccess.Repository;

public class CategoryRepository
{
    public List<Category> Categories { get; set; } = new()
    {
        new()
        {
            Id = 1,
            Name = "Everything"
        }
    };
}