using System.Text.Json.Serialization;

namespace HACS.Models.DonorManagement;

public class Donor : User
{
    public List<Donation> DonationHistory { get; set; }
    
    public Donor() {}
    
    public Donor(string firstName, string lastName, string email, string password, 
        string? secondName = null, Guid id = new()
    ) : base(firstName, lastName, email, password, secondName, id)
    {
        DonationHistory = [];
    }
    
    public Donor(string firstName, string lastName, string email, string password, List<Donation> donationHistory, 
        string? secondName = null, Guid id = new()
        ) : base(firstName, lastName, email, password, secondName, id)
    {
        DonationHistory = donationHistory;
    }
}