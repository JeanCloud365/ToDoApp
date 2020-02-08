using System;
using System.Collections.Generic;
using ToDoApp.Domain.Common;

namespace ToDoApp.Domain.Entities
{
    public class ToDoUser : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public virtual ICollection<WebHook> Webhooks { get; set; }
        public virtual ICollection<ToDoItem> Items { get; set; }

        public ToDoUser()
        {
            Webhooks = new List<WebHook>();
            Items = new List<ToDoItem>();
        }
    }
}