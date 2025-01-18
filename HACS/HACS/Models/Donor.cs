namespace HACS.Models;

public class Donor : User
{
    public Donor()
    {
    }

    public Donor(string firstName, string lastName, string userName, string email,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, userName, email, middleName, id)
    {
        DonationHistory = [];
    }

    public Donor(string firstName, string lastName, string userName, string email, List<Donation> donationHistory,
        string? middleName = null, Guid id = default
    ) : base(firstName, lastName, userName, email, middleName, id)
    {
        DonationHistory = donationHistory;
    }

    public List<Donation> DonationHistory { get; set; }
}