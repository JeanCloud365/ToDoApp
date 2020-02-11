using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions.Impl;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUserWebhook
{
    public class GetToDoUserWebhookQuery:IRequest<GetTodoUserWebhookViewModel>
    {
        public class Handler : IRequestHandler<GetToDoUserWebhookQuery, GetTodoUserWebhookViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public  Handler(IToDoDbContext toDoDbContext,ICurrentUserService currentUserService, IMapper mapper)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<GetTodoUserWebhookViewModel> Handle(GetToDoUserWebhookQuery request, CancellationToken cancellationToken)
            {
                var currentUser = await _toDoDbContext.ToDoUsers.Include(o => o.Webhooks).FirstOrDefaultAsync(o => o.Id.Equals(_currentUserService.Id));
                if (currentUser == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                return new GetTodoUserWebhookViewModel()
                {
                    Items = currentUser.Webhooks.AsQueryable().ProjectTo<GetToDoUserWebhookDto>(_mapper.ConfigurationProvider).ToList()
                };
            }
        }
    }
}
