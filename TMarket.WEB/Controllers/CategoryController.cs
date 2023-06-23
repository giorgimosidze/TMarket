using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMarket.WEB.Helpers.Constants;
using TMarket.WEB.Commands.CategoryCommands;
using TMarket.WEB.Queries.CategoryQueries;

namespace TMarket.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var query = new GetAllCategoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var query = new GetCategoryQuery(id);
            var result = await _mediator.Send(query);
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(string.Format(ModelConstants.PropertyNotFoundFromController, "კატეგორია"));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryRequestCommand userRequest)
        {
            var command = new CategoryUpdateCommand(userRequest, id);
            var result = await _mediator.Send(command);
            return result != null
                ? (IActionResult)Ok(result)
                : NotFound(string.Format(ModelConstants.PropertyNotFoundFromController, "კატეგორია"));

        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction("GetCategory", new { id = result.Id }, result);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new CategoryDeleteCommand(id);
            var result = await _mediator.Send(command);
            return result != null
                ? (IActionResult)Ok(result)
                : BadRequest(string.Format(ModelConstants.PropertyNotFoundFromController, "კატეგორია"));
        }
    }
}
