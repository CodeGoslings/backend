using HACS.Dtos.DonationAdmin;
using HACS.Models;

namespace HACS.Mappers;

public static class DonationAdminMapper
{
    public static GetDonationAdminDto Map(this DonationAdmin donor)
    {
        var donationAdminDto = new GetDonationAdminDto
        {
            Id = Guid.Parse(donor.Id),
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            UserName = donor.UserName,
            Email = donor.Email
        };
        return donationAdminDto;
    }

    public static DonationAdmin Map(this PostDonationAdminDto dto, Guid? id = null)
    {
        var adminId = id ?? Guid.NewGuid();
        return new DonationAdmin(dto.FirstName, dto.LastName, dto.UserName, dto.Email, dto.MiddleName, adminId);
    }
}