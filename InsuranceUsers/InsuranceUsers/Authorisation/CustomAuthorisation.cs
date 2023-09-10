using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InsuranceUsers.Authorisation
{
    public class CustomAuthorisation : AuthorizationHandler<Login>
    {
        private readonly ILogger<CustomAuthorisation> _logger;

        public CustomAuthorisation(ILogger<CustomAuthorisation> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Login requirement)
        {
            _logger.LogWarning("Evaluating authorization requirement for login ", requirement);

            var LoggedIn = context.User.FindFirst(c => c.Type == ClaimTypes.AuthorizationDecision);
            if (LoggedIn.Value == "Authorised")
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("No DateOfBirth claim present");
            }

            return Task.CompletedTask;
        }
    }
}
