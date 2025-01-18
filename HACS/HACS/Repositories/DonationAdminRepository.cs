using HACS.Interfaces;
using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories;

public class DonationAdminRepository(UserManager<DonationAdmin> userManager) : IRepository<DonationAdmin>
{
    public async Task<List<DonationAdmin>> GetAllAsync()
    {
        return await userManager.Users.Include(x => x.ReviewedDonations).ToListAsync();
    }

    public async Task<DonationAdmin?> GetByIdAsync(Guid id)
    {
        return await userManager.FindByIdAsync(id.ToString());
    }

    public async Task<DonationAdmin?> CreateAsync(DonationAdmin donationAdmin, string? password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var result = await userManager.CreateAsync(donationAdmin, password);
        await userManager.AddToRoleAsync(donationAdmin, "DonationAdmin");
        return result.Succeeded ? donationAdmin : null;
    }

    public async Task<DonationAdmin?> UpdateAsync(DonationAdmin donationAdmin)
    {
        var result = await userManager.UpdateAsync(donationAdmin);
        return result.Succeeded ? donationAdmin : null;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null) throw new ArgumentException("User not found");
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new Exception("Failed to delete user");
    }
}