using HACS.Dtos.DonorManagement;
using HACS.Models.DonorManagement;

namespace HACS.Mappers.DonorManagement;

public static class DonationAdminMapper
{
    public static DonationAdminDto Map(this DonationAdmin donor)
    {
        var donationAdminDto = new DonationAdminDto()
        {
            Id = donor.Id,
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            Email = donor.Email
        };
        return donationAdminDto;
    }
    
    public static DonationAdmin Map(this DonationAdminDto dto)
    {
        var donationAdminObj = new DonationAdmin
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Email = dto.Email
        };
        return donationAdminObj;
    }
}