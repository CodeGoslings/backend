namespace HACS.Models;

public abstract class Donation(
    int id,
    DonationType type,
    DonationStatus status,
    DateTime date,
    double amount,
    string description,
    string location)
{
    public int GetId(){return id;}
    public DonationType GetDonationType(){return type;}
    public DonationStatus GetStatus(){return status;}
    public DateTime GetDate(){return date;}
    public double GetAmount(){return amount;}
    public string Description(){return description;}
    public string Location(){return location;}
}

public enum DonationStatus
{
    Pending,
    Accepted,
    Declined,
}

public enum DonationType
{
    Financial,
    Material
}