using HACS.Dtos.DonorManagement;
using HACS.Dtos.DonorManagement.Donation;
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
        return new Donation((DonationType)dto.Type, (DonationStatus)dto.Status, dto.Date, dto.Amount,
            dto.Description, dto.Location, dto.Id);
    }
}