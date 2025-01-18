using HACS.Interfaces;
using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories;

public class DonorRepository(UserManager<Donor> userManager) : IRepository<Donor>
{
    public async Task<List<Donor>> GetAllAsync()
    {
        return await userManager.Users.Include(x => x.DonationHistory).ToListAsync();
    }

    public async Task<Donor?> GetByIdAsync(Guid id)
    {
        return await userManager.FindByIdAsync(id.ToString());
    }

    public async Task<Donor?> CreateAsync(Donor donor, string? password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var result = await userManager.CreateAsync(donor, password);
        await userManager.AddToRoleAsync(donor, "Donor");
        return result.Succeeded ? donor : null;
    }

    public async Task<Donor?> UpdateAsync(Donor donor)
    {
        var result = await userManager.UpdateAsync(donor);
        return result.Succeeded ? donor : null;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null) throw new ArgumentException("User not found");
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new Exception("Failed to delete user");
    }
}