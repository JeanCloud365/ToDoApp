using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ToDoApp.Application.Common.Mappings;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoUsers.Commands.AddToDoUserWebhook
{
    public class AddToDoUserWebhookDto:IMapFrom<WebHook>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WebHook, AddToDoUserWebhookDto>().ForMember(o => o.Id, o => o.MapFrom(a => a.Id));

        }
    }
}
