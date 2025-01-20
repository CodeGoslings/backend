
using HACS.Models;
using Interfaces;

namespace MRCModel.Models;
public class AffectedIndividual : User, IAffectedIndividual
{
    // Additional fields
    public string userLocation { get; set; }
    public string userContactInfo { get; set; }

    // Constructor
    public AffectedIndividual(string userId, string userName, string userEmail, string userPassword, string userLocation, string userContactInfo)
        : base(userId, userName, userEmail, userPassword)
    {
        this.userLocation = userLocation;
        this.userContactInfo = userContactInfo;
    }
    public AffectedIndividual() { }
}