using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories.DonorManagement;

public class DonationAdminRepository(ApplicationDBContext context) : IRepository<DonationAdmin>
{
    public async Task<List<DonationAdmin>> GetAllAsync()
    {
        return await context.DonationAdmins.Include(x => x.ReviewedDonations).ToListAsync();
    }

    public async Task<DonationAdmin?> GetByIdAsync(Guid id)
    {
        return await context.DonationAdmins.Include(x => x.ReviewedDonations).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DonationAdmin?> CreateAsync(DonationAdmin donationAdmin)
    {
        await context.DonationAdmins.AddAsync(donationAdmin);
        await context.SaveChangesAsync();
        return donationAdmin;
    }

    public async Task<DonationAdmin?> UpdateAsync(DonationAdmin donationAdmin)
    {
        var existingDonationAdmin = await context.DonationAdmins.Include(x => x.ReviewedDonations).FirstOrDefaultAsync(
            x => x.Id == donationAdmin.Id);
        if (existingDonationAdmin == null) return null;

        existingDonationAdmin.FirstName = donationAdmin.FirstName;
        existingDonationAdmin.MiddleName = donationAdmin.MiddleName;
        existingDonationAdmin.LastName = donationAdmin.LastName;
        existingDonationAdmin.Email = donationAdmin.Email;
        existingDonationAdmin.PasswordHash = donationAdmin.PasswordHash;
        existingDonationAdmin.ReviewedDonations = donationAdmin.ReviewedDonations;
        
        await context.SaveChangesAsync();
        return existingDonationAdmin;
    }

    public async Task<DonationAdmin?> DeleteAsync(Guid id)
    {
        var donationAdmin = await context.DonationAdmins.Include(x => x.ReviewedDonations).FirstOrDefaultAsync(x => x.Id == id);
        if (donationAdmin == null) return null;
        
        context.DonationAdmins.Remove(donationAdmin);
        await context.SaveChangesAsync();
        return donationAdmin;
    }
}