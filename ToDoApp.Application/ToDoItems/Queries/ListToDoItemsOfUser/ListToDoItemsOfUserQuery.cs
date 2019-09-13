using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser
{
    public class ListToDoItemsOfUserQuery:IRequest<ListToDoItemsOfUserViewModel>
    {
        public class Handler : IRequestHandler<ListToDoItemsOfUserQuery, ListToDoItemsOfUserViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUser _currentUser;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUser currentUser)
            {
                _toDoDbContext = toDoDbContext;
                _currentUser = currentUser;
            }
            public async Task<ListToDoItemsOfUserViewModel> Handle(ListToDoItemsOfUserQuery request, CancellationToken cancellationToken)
            {
                var todos = _toDoDbContext.ToDoItems.Where(a => a.User.Id.Equals(_currentUser.Id)).Select(o => new ListToDoItemsOfUserDto()
                {
                    Description = o.Description,
                    Status = o.Status.Name,
                    Title = o.Title,
                    Id = o.Id
           
                });
                
                return new ListToDoItemsOfUserViewModel()
                {
                    Items = todos
                };
            }
        }
    }
}