using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoApp.Application.Common.Exceptions;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook
{
    public class AddToDoUserWebhookCommand:IRequest<AddToDoUserWebhookViewModel>
    {
      
        public Uri WebhookUrl { get; set; }

        public class Handler : IRequestHandler<AddToDoUserWebhookCommand, AddToDoUserWebhookViewModel>
        {
            private readonly IToDoDbContext _toDoDbContext;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(IToDoDbContext toDoDbContext, ICurrentUserService currentUserService,IMapper mapper)
            {
                _toDoDbContext = toDoDbContext;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }
            public async Task<AddToDoUserWebhookViewModel> Handle(AddToDoUserWebhookCommand request, CancellationToken cancellationToken)
            {
                var todoUser = await _toDoDbContext.ToDoUsers.FindAsync(_currentUserService.Id);

                if (todoUser == null)
                {
                    throw new ItemNotFoundException("User not found");
                }

                if (!todoUser.Id.Equals(_currentUserService.Id))
                {
                    throw new AccessDeniedException("User not current user");
                }

                var webhook = new WebHook()
                {
                    Url = request.WebhookUrl
                };

                todoUser.Webhooks.Add(webhook);

                _toDoDbContext.ToDoUsers.Update(todoUser);

                await _toDoDbContext.SaveChangesAsync(cancellationToken);

                return new AddToDoUserWebhookViewModel()
                {
                    Item = _mapper.Map<AddToDoUserWebhookDto>(webhook)
                };
            }
        }
    }
}
