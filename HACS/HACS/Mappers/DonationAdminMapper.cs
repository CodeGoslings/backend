using HACS.Dtos.DonationAdmin;
using HACS.Models;

namespace HACS.Mappers;

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
        return new DonationAdmin(dto.FirstName, dto.LastName, dto.Email, dto.MiddleName, id);
    }
}