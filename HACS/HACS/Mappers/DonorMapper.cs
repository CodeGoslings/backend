using HACS.Dtos.Donor;
using HACS.Models;

namespace HACS.Mappers;

public static class DonorMapper
{
    public static GetDonorDto Map(this Donor donor)
    {
        var donorDto = new GetDonorDto
        {
            Id = donor.Id,
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            Email = donor.Email
        };
        return donorDto;
    }

    public static Donor Map(this PostDonorDto dto, Guid id = default)
    {
        return new Donor(dto.FirstName, dto.LastName, dto.Email, dto.MiddleName, id);
    }
}