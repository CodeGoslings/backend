namespace MRCModel.Models;
using Interfaces;
using Interfaces;

public class GovernmentRepresentative : User, IGovernmentRepresentative
{
    // Additional fields
    public string userRegion { get; set; }
    public string userAuthorityLevel { get; set; }

    // Constructor
    public GovernmentRepresentative(string userId, string userName, string userEmail, string userPassword, string userRegion, string userAuthorityLevel)
        : base(userId, userName, userEmail, userPassword)
    {
        this.userRegion = userRegion;
        this.userAuthorityLevel = userAuthorityLevel;
    }
    public GovernmentRepresentative() { }
}