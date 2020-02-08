using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enumerations;
using ToDoApp.Domain.Infrastructure;

namespace ToDoApp.Application.ToDoItems.Commands.UpdateToDoItem
{
    public class UpdateToDoItemCommand:IRequest
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<UpdateToDoItemCommand, Unit>
        {

            private readonly ICurrentUserService _currentUser;
            private readonly IToDoDbContext _toDoDbContext;
            private readonly IMediator _mediator;

            public Handler(ICurrentUserService currentUser, IToDoDbContext toDoDbContext,IMediator mediator)
            {
                _currentUser = currentUser;
                _toDoDbContext = toDoDbContext;
                _mediator = mediator;
            }
            public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
            {
                var user = await _toDoDbContext.ToDoUsers.Include(a => a.Items).FirstOrDefaultAsync(o => o.Id.Equals(_currentUser.Id));
                if (user == null)
                {
                    throw new ItemNotFoundException("User does not exist");
                }

                if (_currentUser.Id != user.Id)
                {
                    throw new AccessDeniedException("User is not current user");
                }

                if(!Enumeration.TryParse(request.Status, out ToDoStatus status))
                {
                    throw new Exception();
                }

                var todo = user.Items.FirstOrDefault(o => o.Id.Equals(request.Id));

                if (todo == null)
                {
                    throw new ItemNotFoundException("Item does not exist");
                }

                
                todo.Description = request.Description;
                todo.Title = request.Title;
                todo.Status = status;
                _toDoDbContext.ToDoUsers.Update(user);
                await _toDoDbContext.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new ToDoItemUpdated()
                {
                    UserId = _currentUser.Id,
                    ToDoId = user.Id
                });
                return Unit.Value;
            }
        }
        
    }
}