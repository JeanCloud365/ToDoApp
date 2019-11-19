using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook;

namespace ToDoApp.Application.ToDoUsers.Commands.RemoveToDoUserWebhook
{
    public class RemoveToDoUserWebhookCommand:IRequest
    {
       

        public class Handler : IRequestHandler<RemoveToDoUserWebhookCommand, Unit>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUserService)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
            }
            public async Task<Unit> Handle(RemoveToDoUserWebhookCommand request, CancellationToken cancellationToken)
            {
                var todoUser = await _toDoDbContext.ToDoUsers.FindAsync(_currentUserService.Id);

                if (todoUser == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                if (!todoUser.Id.Equals(_currentUserService.Id))
                {
                    throw new AccessDeniedException("User not current user");
                }

                todoUser.WebhookUrl = null;

                _toDoDbContext.ToDoUsers.Update(todoUser);

                await _toDoDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
