using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;

namespace ToDoApp.Api.Controllers.Admin
{
   
    [Authorize]

    public class UsersController : BaseController
    {
       
        /// <summary>
        /// Shows all todo users (admin permissions required)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OpenApiOperation("getAdminUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<ActionResult<ListToDoUsersViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoUsersQuery()));
        }
        
      
    }
}