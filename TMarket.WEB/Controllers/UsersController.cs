using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMarket.WEB.Helpers.Constants;
using TMarket.WEB.Commands.UserCommands;
using TMarket.WEB.Helpers.CustomExceptions;
using TMarket.WEB.Queries.UserQueries;

namespace TMarket.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetAllUserQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserQuery(id);
            var result = await _mediator.Send(query);
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(string.Format(ModelConstants.PropertyNotFoundFromController, "იუზერი"));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UserRequestCommand userRequest)
        {
            var command = new UserUpdateCommand(userRequest, id);
            var result = await _mediator.Send(command);
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(string.Format(ModelConstants.PropertyNotFoundFromController, "იუზერი"));

        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser(UserRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetUser", new {id = result.Id}, result);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new UserDeleteCommand(id);
            var result = await _mediator.Send(command);
            return result != null 
                ? (IActionResult)Ok(result) 
                : BadRequest(string.Format(ModelConstants.PropertyNotFoundFromController, "იუზერი"));
        }

        // api/Users/GetPaginatedResult?{query}
        [HttpGet("GetPaginatedResult")]
        public async Task<IActionResult> GetPaginatedResult (int? currentPage, int? pageSize, string sortBy, bool? isAsc)
        {
            try
            {
                var query = new GetPaginatedUserQuery(currentPage, pageSize, sortBy, isAsc);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (InvalidArgumentException)
            {
                return BadRequest(ModelConstants.InvalidQuery);
            }
        }
    }
}
