using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUser
{
    public class GetToDoUserQuery:IRequest<GetToDoUserQueryViewModel>
    {
        public Guid Id { get; set; }

        public class Handler:IRequestHandler<GetToDoUserQuery,GetToDoUserQueryViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUserService)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
            }

            public async Task<GetToDoUserQueryViewModel> Handle(GetToDoUserQuery request, CancellationToken cancellationToken)
            {
                var id = request.Id == Guid.Empty ? _currentUserService.Id : request.Id;
                var user = await _toDoDbContext.ToDoUsers.FindAsync(id);

                if (user == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                if (!user.Id.Equals(_currentUserService.Id))
                {
                    throw new AccessDeniedException("User not current user");
                }

                return new GetToDoUserQueryViewModel()
                {
                    User = new GetToDoUserQueryDto()
                    {
                        Id = user.Id,
                        Mail = user.Mail,
                        Name = user.Name
                    }
                };
            }
        }
    }
}
