using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook;

namespace ToDoApp.Application.ToDoUsers.Commands.RemoveToDoUserWebhook
{
    public class RemoveToDoUserWebhookCommand:IRequest
    {

        public Guid Id { get; set; }
       

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
                var todoUser = await _toDoDbContext.ToDoUsers.Include(o => o.Webhooks).FirstOrDefaultAsync(o => o.Id.Equals(_currentUserService.Id),cancellationToken);

                if (todoUser == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                if (!todoUser.Id.Equals(_currentUserService.Id))
                {
                    throw new AccessDeniedException("User not current user");
                }

                var webhook = todoUser.Webhooks.FirstOrDefault(o => o.Id.Equals(request.Id));

                if(webhook == null)
                {
                    throw new ItemNotFoundException("Webhook not found");
                }

                todoUser.Webhooks.Remove(webhook);

                _toDoDbContext.ToDoUsers.Update(todoUser);

                await _toDoDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
