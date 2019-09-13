using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommand:IRequest<CreateToDoItemViewModel>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateToDoItemCommand, CreateToDoItemViewModel>
        {

            private ICurrentUser _currentUser;
            private IToDoDbContext _toDoDbContext;

            public Handler(ICurrentUser currentUser, IToDoDbContext toDoDbContext)
            {
                _currentUser = currentUser;
                _toDoDbContext = toDoDbContext;
            }
            public async Task<CreateToDoItemViewModel> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
            {
                var user = await _toDoDbContext.ToDoUsers.FindAsync(_currentUser.Id);
                if (user == null)
                {
                    
                }

                var entity = new ToDoItem()
                {
                    Description = request.Description,
                    Title = request.Title,
                    User = user
                };
                var createdTodoItem = _toDoDbContext.ToDoItems.Add(entity);
                await _toDoDbContext.SaveChangesAsync(cancellationToken);
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