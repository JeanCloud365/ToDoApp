using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoItems.Queries.ListToDoItems
{
    public class ListToDoItemsQuery:IRequest<ListToDoItemsViewModel>
    {
        public class Handler : IRequestHandler<ListToDoItemsQuery, ListToDoItemsViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUser;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUser)
            {
                _toDoDbContext = toDoDbContext;
                _currentUser = currentUser;
            }
            public async Task<ListToDoItemsViewModel> Handle(ListToDoItemsQuery request, CancellationToken cancellationToken)
            {
                var todos = _toDoDbContext.ToDoItems.Select(o => new ListToDoItemsDto()
                {
                    Description = o.Description,
                    Status = o.Status.Name,
                    Title = o.Title
                });
                
                return new ListToDoItemsViewModel()
                {
                    Items = todos
                };
            }
        }
    }
}