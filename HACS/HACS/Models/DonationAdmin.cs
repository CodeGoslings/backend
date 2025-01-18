namespace HACS.Models;

public class DonationAdmin : User
{
    public DonationAdmin()
    {
    }

    public DonationAdmin(string firstName, string lastName, string email,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, email, middleName, id)
    {
        ReviewedDonations = [];
    }

    public DonationAdmin(string firstName, string lastName, string email,
        List<Donation> reviewedDonations,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, email, middleName, id)
    {
        ReviewedDonations = reviewedDonations;
    }

    public List<Donation> ReviewedDonations { get; set; }
}