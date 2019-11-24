using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var todo = await _toDoDbContext.ToDoItems.Include(a => a.User).FirstOrDefaultAsync(o => o.Id.Equals(request.Id));
                if (todo == null)
                {
                    throw new Exception();
                }

                if (_currentUser.Id != todo.User.Id)
                {
                    throw new Exception();
                }

                if(!Enumeration.TryParse(request.Status, out ToDoStatus status))
                {
                    throw new Exception();
                }

                todo.Id = request.Id;
                todo.Description = request.Description;
                todo.Title = request.Title;
                todo.Status = status;
                _toDoDbContext.ToDoItems.Update(todo);
                await _toDoDbContext.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new ToDoItemUpdated()
                {
                    UserId = _currentUser.Id,
                    ToDoId = todo.Id
                });
                return Unit.Value;
            }
        }
        
    }
}