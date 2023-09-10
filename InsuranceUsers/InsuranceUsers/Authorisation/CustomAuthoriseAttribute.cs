using Microsoft.AspNetCore.Authorization;

namespace InsuranceUsers.Authorisation
{
    internal class CustomAuthoriseAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "AuthorisedUser";

        public CustomAuthoriseAttribute(string userRole) => UserRole = userRole;

        // Get or set the Age property by manipulating the underlying Policy property
        public string UserRole
        {
            get
            {
                if (Roles.Split(',').Any(x => x.Equals(UserRole)))
                {
                    return UserRole;
                }
                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(Roles))
                {
                    Roles = string.Empty;
                }
                Roles += $",{value}";
                Roles.TrimStart(',');
            }
        }
    }
}
