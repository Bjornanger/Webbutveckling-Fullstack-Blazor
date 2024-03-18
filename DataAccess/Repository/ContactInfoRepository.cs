using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace DataAccess.Repository;

public class ContactInfoRepository : IContactInfoService<ContactInfo>
{
    private readonly GroceryStoreDbContext _context;

    public ContactInfoRepository(GroceryStoreDbContext context)
    {
        _context = context;
    }
    

    public async Task<IEnumerable<ContactInfo>> GetAllAsync()
    {
        return _context.ContactInfos;
    }

    public async Task<ContactInfo> GetByIdAsync(int id)
    {
        return await _context.ContactInfos.FindAsync(id);
    }

    public async Task AddAsync(ContactInfo contactInfo)
    { 
        await _context.ContactInfos.AddAsync(contactInfo);
        await _context.SaveChangesAsync();
    }
    
   

    public async Task<ContactInfo> UpdateCustomerInfo(int id, ContactInfo contactInfo)
    {
        var contactInfoToUpdate = await _context.ContactInfos.FindAsync(id);

        if (contactInfoToUpdate is null)
        {
            return null;
        }
        contactInfoToUpdate.Address = contactInfo.Address;
        contactInfoToUpdate.City = contactInfo.City;
        contactInfoToUpdate.Country = contactInfo.Country;
        contactInfoToUpdate.Phone = contactInfo.Phone;
        contactInfoToUpdate.ZipCode = contactInfo.ZipCode;
        contactInfoToUpdate.Region = contactInfo.Region;
        await _context.SaveChangesAsync();
        return contactInfoToUpdate;
    }

}