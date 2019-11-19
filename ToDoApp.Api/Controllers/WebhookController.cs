using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItems;
using ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Commands.RemoveToDoUserWebhook;
using ToDoApp.Application.ToDoUsers.Queries.GetToDoUserWebhook;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;

namespace ToDoApp.Api.Controllers
{

    public class WebhookController : BaseController
    {

        [HttpPost()]
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(AddToDoUserWebhookCommand command)
        {
            await Mediator.Send(command);
            return CreatedAtAction("Delete",null);
        }

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete()
        {
            await Mediator.Send(new RemoveToDoUserWebhookCommand()
            {
              
            });

            return Ok();
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetTodoUserWebhookViewModel> Get()
        {
            var ret = await Mediator.Send(new GetToDoUserWebhookQuery()
            {

            });

            return ret;
        }


    }
}