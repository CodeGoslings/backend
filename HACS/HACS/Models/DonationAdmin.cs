namespace HACS.Models;

public class DonationAdmin(
    int id,
    string firstName, 
    string? secondName, 
    string lastName, 
    string email, 
    string password, 
    List<Donation> donationsReviewed) : User(id, firstName, secondName, lastName, email, password)
{
    private readonly List<Donation> _donationsReviewed = donationsReviewed;
    public List<Donation> GetDonationsReviewed(){return _donationsReviewed;} 
}