using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook
{
    public class AddToDoUserWebhookCommand:IRequest
    {
      
        public Uri WebhookUrl { get; set; }

        public class Handler : IRequestHandler<AddToDoUserWebhookCommand, Unit>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUserService)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
            }
            public async Task<Unit> Handle(AddToDoUserWebhookCommand request, CancellationToken cancellationToken)
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

                todoUser.WebhookUrl = request.WebhookUrl;

                _toDoDbContext.ToDoUsers.Update(todoUser);

                await _toDoDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
