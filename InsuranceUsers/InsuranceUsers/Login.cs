using Microsoft.AspNetCore.Authorization;

namespace InsuranceUsers
{
    public class Login : IAuthorizationRequirement
    {
        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}