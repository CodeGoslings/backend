using HACS.Dtos.Donation;
using HACS.Models;

namespace HACS.Mappers;

public static class DonationMapper
{
    public static GetDonationDto Map(this Donation donation)
    {
        var donationDto = new GetDonationDto
        {
            Id = donation.Id,
            Type = (int)donation.Type,
            Status = (int)donation.Status,
            Date = donation.Date,
            Amount = donation.Amount,
            Description = donation.Description,
            Location = donation.Location
        };
        return donationDto;
    }

    public static Donation Map(this PostDonationDto dto, Guid id = default)
    {
        return new Donation((DonationType)dto.Type, (DonationStatus)dto.Status, dto.Date, dto.Amount,
            dto.Description, dto.Location, id);
    }
}