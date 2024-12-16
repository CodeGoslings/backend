using System.Diagnostics.CodeAnalysis;
using HACS.Dtos.DonorManagement;
using HACS.Dtos.DonorManagement.Donor;
using HACS.Helpers.DonorManagement;
using HACS.Models.DonorManagement;

namespace HACS.Mappers.DonorManagement;

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
        return new Donor(dto.FirstName, dto.LastName, dto.Email, HashHelper.HashPassword(dto.Password),
            dto.MiddleName, id);
    }
}