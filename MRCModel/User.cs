using System.Text.Json.Nodes;
namespace MRCModel

{
    public class User
    {
        // Fields
        public string userId { get; set; }
        private string userName { get; set; }
        private string userEmail { get; set; }
        private string userPassword { get; set; }

        // Constructor
        public User(string userId, string userName, string userEmail, string userPassword)
        {
            this.userId = userId;
            this.userName = userName;  // Use the field directly
            this.userEmail = userEmail;
            this.userPassword = userPassword;
        }

        // Method to convert the object to a JSON object
        public JsonObject ToJsonObject()
        {
            return new JsonObject
            {
                { "userId", this.userId },
                { "userName", this.userName }
            };
        }
    }
}
