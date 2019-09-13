using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers
{
    public class ListToDoUsersQuery:IRequest<ListToDoUsersViewModel>
    {
        public class Handler : IRequestHandler<ListToDoUsersQuery, ListToDoUsersViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUser _currentUser;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUser currentUser)
            {
                _toDoDbContext = toDoDbContext;
                _currentUser = currentUser;
            }
            public async Task<ListToDoUsersViewModel> Handle(ListToDoUsersQuery request, CancellationToken cancellationToken)
            {
                var users = _toDoDbContext.ToDoUsers.Select(o => new ListToDoUsersDto()
                {
                    Name = o.Name,
                    Id = o.Id,
                    Mail = o.Mail
                });
                
                return new ListToDoUsersViewModel()
                {
                    Items = users
                };
            }
        }
    }
}