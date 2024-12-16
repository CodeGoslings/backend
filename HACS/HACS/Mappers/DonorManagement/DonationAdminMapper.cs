using HACS.Dtos.DonorManagement;
using HACS.Dtos.DonorManagement.DonationAdmin;
using HACS.Dtos.DonorManagement.Donor;
using HACS.Helpers.DonorManagement;
using HACS.Models.DonorManagement;

namespace HACS.Mappers.DonorManagement;

public static class DonationAdminMapper
{
    public static GetDonationAdminDto Map(this DonationAdmin donor)
    {
        var donationAdminDto = new GetDonationAdminDto
        {
            Id = donor.Id,
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            Email = donor.Email
        };
        return donationAdminDto;
    }
    
    public static DonationAdmin Map(this PostDonationAdminDto dto, Guid id = default)
    {
        return new DonationAdmin(dto.FirstName, dto.LastName, dto.Email, HashHelper.HashPassword(dto.Password),
            dto.MiddleName, id);
    }
}