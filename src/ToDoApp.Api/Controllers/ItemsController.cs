using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.Annotations;
using ToDoApp.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem;
using ToDoApp.Application.ToDoItems.Queries.GetToDoItem;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Api.Controllers
{
    public class ItemsController : BaseController
    {
        /// <summary>
        /// List my todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [OpenApiOperation("getTodos")]
        public async Task<ActionResult<ListToDoItemsOfUserViewModel>> List()
        {
            return Ok(await Mediator.Send(new ListToDoItemsOfUserQuery()));
        }
        /// <summary>
        /// Retrieves a single todo
        /// </summary>
        /// <param name="id">The id of the todo to retrieve</param>
        /// <example>
        ///
        /// </example>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [OpenApiOperation("getTodoById")]
        public async Task<ActionResult<GetToDoItemViewModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetToDoItemQuery
            {
                ItemId = new Guid(id)
            }));
        }

        /// <summary>
        /// Creates a new todo
        /// </summary>
        /// <param name="title">Title of the new todo</param>
        /// <param name="description">Description of the new todo</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [OpenApiOperation("createTodo")]
        public async Task<ActionResult<CreateToDoItemViewModel>> Create(CreateToDoItemCommand command)
        {
           
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update an existing todo item
        /// </summary>
        /// <param name="id">Id of the todo to update</param>
        /// <param name="title">new title</param>
        /// <param name="description">new description</param>
        /// <param name="status">new status</param>
        [OpenApiOperation("updateTodo")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateToDoItemCommand command)
        {
            
            await Mediator.Send(command);
            return Ok();
        }


    }
}