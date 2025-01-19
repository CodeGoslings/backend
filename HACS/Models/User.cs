using Interfaces;
using System.Text.Json.Nodes;
namespace MRCModel.Models

{
    public class User: IUser
    {
        // Fields
        public string userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }

        // Constructor
        public User(string userId, string userName, string userEmail, string userPassword)
        {
            this.userId = userId;
            this.userName = userName;  // Use the field directly
            this.userEmail = userEmail;
            this.userPassword = userPassword;
        }
        public User() { }
        // Method to convert the object to a JSON object
        public JsonObject ToJsonObject()
        {
            return new JsonObject
            {
                { "userId", userId },
                { "userName", userName }
            };
        }
    }
}
