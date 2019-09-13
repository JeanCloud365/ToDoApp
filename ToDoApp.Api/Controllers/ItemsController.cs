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
using ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListToDoItemsOfUserViewModel>> List()
        {
            return Ok(await _mediator.Send(new ListToDoItemsOfUserQuery()));
        }
        
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateToDoItemViewModel>> Create(CreateToDoItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]   
        public async Task<IActionResult> Update(UpdateToDoItemCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        
        
    }
}