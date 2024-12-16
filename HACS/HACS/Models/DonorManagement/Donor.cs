namespace HACS.Models.DonorManagement;

public class Donor : User
{
    public List<Donation> DonationHistory { get; set; }
    
    public Donor() {}
    
    public Donor(string firstName, string lastName, string email, string password, 
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, email, password, middleName, id)
    {
        DonationHistory = [];
    }
    
    public Donor(string firstName, string lastName, string email, string password, List<Donation> donationHistory, 
        string? middleName = null, Guid id = default
        ) : base(firstName, lastName, email, password, middleName, id)
    {
        DonationHistory = donationHistory;
    }
}