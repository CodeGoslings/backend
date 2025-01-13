namespace MRCModel;

public class AidOrganizationWorker : User
{
    // Additional field
    public string role { get; set; }

    // Constructor
    public AidOrganizationWorker(string userId, string userName, string userEmail, string userPassword, string role)
        :base(userId, userName, userEmail, userPassword)
    {
        this.role = role;
    }
}