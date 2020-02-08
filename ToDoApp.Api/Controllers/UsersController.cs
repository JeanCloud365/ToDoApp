using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using ToDoApp.Application.ToDoItems.Queries.ListToDoItems;
using ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.GetToDoUser;
using ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers;

namespace ToDoApp.Api.Controllers
{

    public class UsersController : BaseController
    {

        [HttpPost()]
        [OpenApiIgnore]
        [ProducesResponseType(StatusCodes.Status200OK)]
       
        public async Task<ActionResult<ListToDoUsersViewModel>> Create()
        {
            return Ok(await Mediator.Send(new CreateToDoUserCommand()));
        }

        /*
        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="id">The id of the user to fetch. Must be same as authenticated user.</param>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetToDoUserQueryViewModel>> Get(Guid id)
        {
            var ret = await Mediator.Send(new GetToDoUserQuery()
            {
                Id = id
            });

            return Ok(ret);

        }
        */



    }
}