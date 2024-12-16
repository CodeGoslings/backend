using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories.DonorManagement;

public class DonorRepository(ApplicationDBContext context) : IRepository<Donor>
{
    public async Task<List<Donor>> GetAllAsync()
    {
        return await context.Donors.Include(x => x.DonationHistory).ToListAsync();
    }

    public async Task<Donor?> GetByIdAsync(Guid id)
    {
        return await context.Donors.Include(x => x.DonationHistory).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Donor?> CreateAsync(Donor donor)
    {
        await context.Donors.AddAsync(donor);
        await context.SaveChangesAsync();
        return donor;
    }

    public async Task<Donor?> UpdateAsync(Donor donor)
    {
        var existingDonor = await context.Donors.Include(x => x.DonationHistory).FirstOrDefaultAsync(x => x.Id == donor.Id);
        if (existingDonor == null) return null;

        existingDonor.FirstName = donor.FirstName;
        existingDonor.MiddleName = donor.MiddleName;
        existingDonor.LastName = donor.LastName;
        existingDonor.Email = donor.Email;
        existingDonor.Password = donor.Password;
        existingDonor.DonationHistory = donor.DonationHistory;
        
        await context.SaveChangesAsync();
        return existingDonor;
    }

    public async Task<Donor?> DeleteAsync(Guid id)
    {
        var donor = await context.Donors.Include(x => x.DonationHistory).FirstOrDefaultAsync(x => x.Id == id);
        if (donor == null) return null;

        context.Donors.Remove(donor);
        await context.SaveChangesAsync();
        return donor;
    }
}