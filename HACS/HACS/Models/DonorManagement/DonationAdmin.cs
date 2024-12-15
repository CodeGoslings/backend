namespace HACS.Models.DonorManagement;

public class DonationAdmin : User
{
    public List<Donation> ReviewedDonations { get; set; }
    
    public DonationAdmin(string firstName, string lastName, string email, string password, 
        string? secondName = null, Guid id = new()
    ) : base(firstName, lastName, email, password, secondName, id)
    {
        ReviewedDonations = [];
    }
    
    public DonationAdmin(string firstName, string lastName, string email, string password, List<Donation> reviewedDonations, 
        string? secondName = null, Guid id = new()
    ) : base(firstName, lastName, email, password, secondName, id)
    {
        ReviewedDonations = reviewedDonations;
    }
}