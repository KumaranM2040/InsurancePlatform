using InsuranceUsers.Repository;
using InsuranceUsers.Repository.Models;
using InsuranceUsers.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsuranceUsers.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IPasswordManager passwordManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordManager = passwordManager;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<Register>>> Get()
        {
            var result = await _userRepository.GetAllUsers();
            return Ok(result.Select(x => new Register { Id = x.UserIndex, Title=x.Title,Name = x.Surname, EmailAddress = x.EmailAddress, Username = x.Username}));
        }

        [HttpGet()]
        [Route("/Find")]
        public async Task<ActionResult<IEnumerable<Register>>> GetByUsername( string Username)
        {
            var result = await _userRepository.GetUserByUsername(Username);
            return Ok(result.Select(x => new Register { Id = x.UserIndex, Title = x.Title, Name = x.Surname, EmailAddress = x.EmailAddress, Username = x.Username }));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Signin(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return BadRequest("A username and password are required");

            // In a real-world application, user credentials would need validated before signing in
            var claims = new List<Claim>();
            // Add a Name claim and, if birth date was provided, a DateOfBirth claim
            claims.Add(new Claim(ClaimTypes.Name, userName));
            var userWithUsername = await _userRepository.GetUserByUsername(userName);
            if (userWithUsername.Count() == 0)
            {
                return BadRequest("Username does not exist");
            }

            var userDetails = userWithUsername.FirstOrDefault();
            var passwordMatch = _passwordManager.VerifyPassword(password, userDetails.PasswordHash, userDetails.PasswordSalt);
            if (passwordMatch)
            {
                claims.Add(new Claim(ClaimTypes.AuthorizationDecision, "Authorized"));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            
            // Create user's identity and sign them in
            var identity = new ClaimsIdentity(claims, "UserSpecified");
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<string>> Post(Register user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var userWithUsername = await _userRepository.GetUserByUsername(user.Username);
                if (userWithUsername.Count() != 0)
                {
                    return BadRequest("Username already exists");
                }

                (var hash, var salt) = _passwordManager.HashPassword(user.Password);
                var userDetails = new UserDetails
                {
                    Title = user.Title,
                    Name = user.Name,
                    Surname = user.Surname,
                    EmailAddress = user.EmailAddress,
                    Username = user.Username,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };
                var registeredCount = await _userRepository.Register(userDetails);
                if (registeredCount > 0)
                {
                    return Ok($"User with {user.Username} was registered successfully" );
                }
                else
                {
                    return BadRequest("User was not registered");
                }
            }
            
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    e.Message);
            }
        }

        [HttpPatch()]
        public async Task<ActionResult<IEnumerable<Register>>> UpdateByUsername(string Username)
        {
            var result = await _userRepository.GetUserByUsername(Username);
            return Ok(result.Select(x => new Register { Id = x.UserIndex, Title = x.Title, Name = x.Surname, EmailAddress = x.EmailAddress, Username = x.Username }));
        }


    }
}
