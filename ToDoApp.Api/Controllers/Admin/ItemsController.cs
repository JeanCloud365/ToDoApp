using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItems;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser;

namespace ToDoApp.Api.Controllers.Admin
{
    
    public class ItemsController : BaseController
    {
       
        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListToDoItemsViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoItemsQuery()));
        }
        
       
        
        
    }
}