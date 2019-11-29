using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Application.Notifications.Models;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommand:IRequest<CreateToDoItemViewModel>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateToDoItemCommand, CreateToDoItemViewModel>
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
            public async Task<CreateToDoItemViewModel> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
            {
                var user = await _toDoDbContext.ToDoUsers.FindAsync(_currentUser.Id);
                if (user == null)
                {
                    throw new AccessDeniedException("Invalid User");
                }

                var entity = new ToDoItem()
                {
                    Description = request.Description,
                    Title = request.Title,
                    User = user
                };
                var createdTodoItem = _toDoDbContext.ToDoItems.Add(entity);
                await _toDoDbContext.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new ToDoItemUpdated()
                {
                    ToDoId = entity.Id,
                    UserId = user.Id
                },cancellationToken);
                return new CreateToDoItemViewModel()
                {
                    ToDoItem = new CreateToDoItemDto()
                    {
                        Id = entity.Id
                    }
                };
            }
        }
        
    }
}