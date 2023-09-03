using Microsoft.AspNetCore.Mvc;

namespace InsuranceUsers.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public IEnumerable<Register> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Register
            {
                Id = index,
                Title = "Mr",
                Name = "John",
                Surname = "Doe",
                EmailAddress = ""
            });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<Register>> Post(Register user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                //var createdEmployee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(Register),
                    new { id = 1 }, null);
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

    }
}
