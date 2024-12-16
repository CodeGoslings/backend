namespace HACS.Models.DonorManagement;

public class DonationAdmin : User
{
    public List<Donation> ReviewedDonations { get; set; }
    
    public DonationAdmin() {}
    
    public DonationAdmin(string firstName, string lastName, string email, string password, 
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, email, password, middleName, id)
    {
        ReviewedDonations = [];
    }
    
    public DonationAdmin(string firstName, string lastName, string email, string password, List<Donation> reviewedDonations, 
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, email, password, middleName, id)
    {
        ReviewedDonations = reviewedDonations;
    }
}