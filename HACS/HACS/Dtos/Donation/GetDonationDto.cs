using System.ComponentModel.DataAnnotations;

namespace HACS.Dtos.Donation;

public class GetDonationDto
{
    public Guid Id { get; set; }

    [AllowedValues(0, 1)] public int Type { get; set; }

    [AllowedValues(0, 1, 2)] public int Status { get; set; }

    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}