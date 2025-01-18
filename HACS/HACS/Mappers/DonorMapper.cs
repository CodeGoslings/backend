using HACS.Dtos.Donor;
using HACS.Models;

namespace HACS.Mappers;

public static class DonorMapper
{
    public static GetDonorDto Map(this Donor donor)
    {
        var donorDto = new GetDonorDto
        {
            Id = Guid.Parse(donor.Id),
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            UserName = donor.UserName,
            Email = donor.Email
        };
        return donorDto;
    }

    public static Donor Map(this PostDonorDto dto, Guid? id = null)
    {
        var adminId = id ?? Guid.NewGuid();
        return new Donor(dto.FirstName, dto.LastName, dto.UserName, dto.Email, dto.MiddleName, adminId);
    }
}