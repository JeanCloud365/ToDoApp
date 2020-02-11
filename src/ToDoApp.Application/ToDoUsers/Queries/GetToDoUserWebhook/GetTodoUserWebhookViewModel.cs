using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Application.Common.Mappings;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUserWebhook
{
    public class GetTodoUserWebhookViewModel
    {
        public ICollection<GetToDoUserWebhookDto> Items { get; set; }

        
    }
}
