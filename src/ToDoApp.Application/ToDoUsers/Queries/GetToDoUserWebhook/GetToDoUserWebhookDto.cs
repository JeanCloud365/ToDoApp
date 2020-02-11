using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ToDoApp.Application.Common.Mappings;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUserWebhook
{
    public class GetToDoUserWebhookDto:IMapFrom<WebHook>
    {
        public string WebhookUrl { get; set; }
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WebHook, GetToDoUserWebhookDto>()
                .ForMember(d => d.WebhookUrl, opt => opt.MapFrom(s => s.Url.ToString()));
        }
    }
}
