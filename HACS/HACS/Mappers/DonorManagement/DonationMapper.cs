using HACS.Dtos.DonorManagement;
using HACS.Models.DonorManagement;

namespace HACS.Mappers.DonorManagement;

public static class DonationMapper
{
    public static DonationDto Map(this Donation donation)
    {
        var donationDto = new DonationDto()
        {
            Id = donation.Id,
            Type = (int)donation.Type,
            Status = (int)donation.Status,
            Date = donation.Date,
            Amount = donation.Amount,
            Description = donation.Description,
            Location = donation.Location,
        };
        return donationDto;
    }
    
    public static Donation Map(this DonationDto dto)
    {
        var donationObj = new Donation
        {
            Id = dto.Id,
            Type = (DonationType)dto.Type,
            Status = (DonationStatus)dto.Status,
            Date = dto.Date,
            Amount = dto.Amount,
            Description = dto.Description,
            Location = dto.Location,
        };
        return donationObj;
    }
}