namespace MRCModel;
public class GovernmentRepresentative : User
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
    }