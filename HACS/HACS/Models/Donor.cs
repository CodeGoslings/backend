namespace HACS.Models;

public class Donor(
    int id,
    string firstName,
    string? secondName,
    string email,
    string password,
    string lastName,
    List<Donation> donationHistory,
    List<Donation> currentDonations) : User(id, firstName, secondName, lastName, email, password)
{
    private List<Donation> _donationHistory = [];
    private List<Donation> _currentDonations = [];
    
    public List<Donation> GetDonationHistory(){return donationHistory;}
    public List<Donation> GetCurrentDonations(){return currentDonations;}
}