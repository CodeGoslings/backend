namespace MRCModel.Models;
using Interfaces;
public class AidOrganizationWorker : User, IAidOrganizationWorker
{
    // Additional field
    public string role { get; set; }

    // Constructor
    public AidOrganizationWorker(string userId, string userName, string userEmail, string userPassword, string role)
        : base(userId, userName, userEmail, userPassword)
    {
        this.role = role;
    }
    public AidOrganizationWorker() { }
}