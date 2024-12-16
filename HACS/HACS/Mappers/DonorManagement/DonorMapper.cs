using System.Diagnostics.CodeAnalysis;
using HACS.Dtos.DonorManagement;
using HACS.Models.DonorManagement;

namespace HACS.Mappers.DonorManagement;

public static class DonorMapper
{
    public static DonorDto Map(this Donor donor)
    {
        var donorDto = new DonorDto
        {
            Id = donor.Id,
            FirstName = donor.FirstName,
            MiddleName = donor.MiddleName,
            LastName = donor.LastName,
            Email = donor.Email
        };
        return donorDto;
    }
    
    public static Donor Map(this DonorDto dto)
    {
        var donorObj = new Donor
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            Email = dto.Email
        };
        return donorObj;
    }
}