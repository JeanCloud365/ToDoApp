using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUserWebhook
{
    public class GetToDoUserWebhookQuery:IRequest<GetTodoUserWebhookViewModel>
    {
        public class Handler : IRequestHandler<GetToDoUserWebhookQuery, GetTodoUserWebhookViewModel>
        {
            private IToDoDbContext _toDoDbContext;
            private ICurrentUserService _currentUserService;

            public  Handler(IToDoDbContext toDoDbContext,ICurrentUserService currentUserService)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
            }
            public async Task<GetTodoUserWebhookViewModel> Handle(GetToDoUserWebhookQuery request, CancellationToken cancellationToken)
            {
                var currentUser = await _toDoDbContext.ToDoUsers.FindAsync(_currentUserService.Id);
                if (currentUser == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                return new GetTodoUserWebhookViewModel()
                {
                    Item = new GetToDoUserWebhookDto()
                    {
                        WebhookUrl = currentUser.WebhookUrl.ToString()
                    }
                };
            }
        }
    }
}
