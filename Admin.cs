namespace CarConnect.Model
{
    public class Admin
    {
        int adminID;
        string firstName;
        string lastName;
        string email;
        string phoneNumber;
        string userName;
        string password;
        string role;
        DateTime joinDate;

        public int AdminID
        {
            get { return adminID; }
            set { adminID = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public DateTime JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; }
        }
        //public Admin(int adminID, string firstName, string lastName, string email, string phoneNumber, string userName, string password, string role, DateTime joinDate)
        //{
        //    AdminID = adminID;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //    UserName = userName;
        //    Password = password;
        //    Role = role;
        //    JoinDate = joinDate;

        //}
        public override string ToString()
        {
            return $"Admin ID:{adminID}, FirstName:{firstName}, LastName:{lastName}, Email:{email}, PhoneNumber:{phoneNumber},UserName:{userName},Password:{password}, Role:{Role}, JoinDate:{joinDate}";
        }


    }
}
