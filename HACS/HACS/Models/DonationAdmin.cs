namespace HACS.Models;

public class DonationAdmin : User
{
    public DonationAdmin()
    {
    }

    public DonationAdmin(string firstName, string lastName, string userName, string email,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, userName, email, middleName, id)
    {
        ReviewedDonations = [];
    }

    public DonationAdmin(string firstName, string lastName, string userName, string email,
        List<Donation> reviewedDonations,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, userName, email, middleName, id)
    {
        ReviewedDonations = reviewedDonations;
    }

    public List<Donation> ReviewedDonations { get; set; }
}