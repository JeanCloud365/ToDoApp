using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
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
        /// <summary>
        /// Register a webhook for todo creations and updates
        /// </summary>
        [HttpPost()]
        [OpenApiOperation("createWebhook")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(AddToDoUserWebhookCommand command)
        {
            await Mediator.Send(command);
            return CreatedAtAction("Delete", null);
        }

        /// <summary>
        /// Remove a webhook
        /// </summary>
        [OpenApiOperation("deleteWebhook")]
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

        /// <summary>
        /// Retrieve current webhook
        /// </summary>
        [OpenApiOperation("getWebhook")]
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