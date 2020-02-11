using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoUsers.Commands.CreateToDoUser
{
    public class CreateToDoUserCommand:IRequest
    {

        public class Handler : IRequestHandler<CreateToDoUserCommand, Unit>
        {

            private readonly ICurrentUserService _currentUser;
            private readonly IToDoDbContext _toDoDbContext;

            public Handler(ICurrentUserService currentUser, IToDoDbContext toDoDbContext)
            {
                _toDoDbContext = toDoDbContext;
                _currentUser = currentUser;
            }

            public async Task<Unit> Handle(CreateToDoUserCommand request, CancellationToken cancellationToken)
            {
                var existing = await _toDoDbContext.ToDoUsers.FindAsync(_currentUser.Id);
               
                var user = new ToDoUser()
                {
                    Id = _currentUser.Id,
                    Mail = _currentUser.Mail,
                    Name = _currentUser.Name
                };


                if (existing == null)
                {
                    _toDoDbContext.ToDoUsers.Add(user);
                }
                else
                {
                    existing.Id = user.Id;
                    existing.Mail = user.Mail;
                    existing.Name = user.Name;
                    _toDoDbContext.ToDoUsers.Update(existing);
                }

                await _toDoDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        
        }
        
    }
}