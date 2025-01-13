namespace MRCModel
{
    public class User
    {
        // Fields
        public string userId { get; set; }
        private string userName;
        private string userEmail;
        private string userPassword;

        // Constructor
        public User(string userId, string userName, string userEmail, string userPassword)
        {
            this.userId = userId;
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.UserPassword = userPassword;
        }

        // Getter and Setter for userName
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        // Getter and Setter for userEmail
        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; }
        }

        // Getter and Setter for userPassword
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
    }
}
