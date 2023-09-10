namespace InsuranceUsers.Repository.Models
{
    public class UserDetails
    {
        public int UserIndex { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
