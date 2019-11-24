using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem;
using ToDoApp.Application.ToDoItems.Queries.GetToDoItem;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser;

namespace ToDoApp.Api.Controllers
{
    public class ItemsController : BaseController
    {
      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListToDoItemsOfUserViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoItemsOfUserQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ListToDoItemsOfUserViewModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetToDoItemQuery
            {
                ItemId = new Guid(id)
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateToDoItemViewModel>> Create(CreateToDoItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]   
        public async Task<IActionResult> Update(UpdateToDoItemCommand command)
        {
                            await Mediator.Send(command);
            return Ok();
        }
        
        
    }
}