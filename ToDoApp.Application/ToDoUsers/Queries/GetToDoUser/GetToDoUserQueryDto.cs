using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ToDoApp.Application.ToDoUsers.Queries.GetToDoUser
{
    public class GetToDoUserQueryDto
    {
        public string Mail { get; set; }
        public Guid Id { get; set; }

        public ICollection<Uri> Webhooks { get; set; }
        public string Name { get; set; }
    }
}
