using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories.DonorManagement;

public class DonationRepository(ApplicationDBContext context) : IRepository<Donation>
{
    public async Task<List<Donation>> GetAllAsync()
    {
        return await context.Donations.ToListAsync();
    }

    public async Task<Donation?> GetByIdAsync(Guid id)
    {
        return await context.Donations.FindAsync(id);
    }

    public async Task<Donation?> CreateAsync(Donation donation)
    {
        await context.Donations.AddAsync(donation);
        await context.SaveChangesAsync();
        return donation;
    }

    public async Task<Donation?> UpdateAsync(Donation donation)
    {
        var existingDonation = await context.Donations.FirstOrDefaultAsync(x => x.Id == donation.Id);
        if (existingDonation == null) return null;

        existingDonation.Type = donation.Type;
        existingDonation.Status = donation.Status;
        existingDonation.Date = donation.Date;
        existingDonation.Amount = donation.Amount;
        existingDonation.Description = donation.Description;
        existingDonation.Location = donation.Location;
        
        await context.SaveChangesAsync();
        return existingDonation;
    }

    public async Task<Donation?> DeleteAsync(Guid id)
    {
        var donation = await context.Donations.FirstOrDefaultAsync(x => x.Id == id);
        if (donation == null) return null;
        
        context.Donations.Remove(donation);
        await context.SaveChangesAsync();
        return donation;
    }
}