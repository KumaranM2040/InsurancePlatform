using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace InsuranceUsers.Authorisation
{
    internal class CustomPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "AuthorisedUser";
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public CustomPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new Login("", ""));
                return Task.FromResult(policy.Build());
            }
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
