using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoItems.Queries.GetToDoItem
{
    public class GetToDoItemQuery : IRequest<GetToDoItemViewModel>
    {
        public Guid ItemId { get; set; }

        public class Handler : IRequestHandler<GetToDoItemQuery, GetToDoItemViewModel>
        {

            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUserService)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
            }
            public async Task<GetToDoItemViewModel> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
            {
                var todoItem = await _toDoDbContext.ToDoItems.Include(o => o.User).FirstOrDefaultAsync(o => o.Id.Equals(request.ItemId),cancellationToken);

                if(todoItem == null)
                {
                    throw new ItemNotFoundException("Item not found");

                }
                
                if (!todoItem.User.Id.Equals(_currentUserService.Id))
                {
                    throw new AccessDeniedException("Item not of user");
                }

                return new GetToDoItemViewModel()
                {
                    Item = new GetToDoItemDto()
                    {
                        Description = todoItem.Description,
                        Status = todoItem.Status.ToString(),
                        Title = todoItem.Title
                    }
                };



            }
        }

    }
}
