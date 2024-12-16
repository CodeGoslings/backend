using System.ComponentModel.DataAnnotations;

namespace HACS.Models.DonorManagement;

public class Donation
{
    [Key]
    public Guid Id { get; set; }
    public DonationType Type { get; set; }
    public DonationStatus Status { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public Donation() {}
    
    public Donation(DonationType type, DonationStatus status, DateTime date, double amount, string description, string location, Guid id = default)
    {
        Id = id;
        Type = type;
        Status = status;
        Date = date;
        Amount = amount;
        Description = description;
        Location = location;
    }
}

public enum DonationStatus
{
    Pending = 0,
    Accepted = 1,
    Declined = 2,
}

public enum DonationType
{
    Financial = 0,
    Material = 1
}