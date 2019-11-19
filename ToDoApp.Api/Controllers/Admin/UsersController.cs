using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;

namespace ToDoApp.Api.Controllers.Admin
{
   
    [Authorize]

    public class UsersController : BaseController
    {
       
        
        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<ActionResult<ListToDoUsersViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoUsersQuery()));
        }
        
      
    }
}