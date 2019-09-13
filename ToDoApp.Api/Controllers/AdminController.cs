using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItems;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("users")]
        [Authorize(Policy = "ReadAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<ActionResult<ListToDoUsersViewModel>> ListAllUsers(ListToDoUsersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        
        [HttpGet("items")]
        [Authorize(Policy = "ReadAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListToDoItemsViewModel>> ListAllItems(ListToDoItemsQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}