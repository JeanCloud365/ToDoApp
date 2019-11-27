using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItems;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser;

namespace ToDoApp.Api.Controllers.Admin
{
    
    public class ItemsController : BaseController
    {
        /// <summary>
        /// Lists all todo items (admin permissions required)
        /// </summary>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [OpenApiOperation("getAdminTodos")]
        public async Task<ActionResult<ListToDoItemsViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoItemsQuery()));
        }
        
       
        
        
    }
}