using System;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Domain.Entities
{
    public class ToDoItem:AuditableEntity
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public ToDoStatus Status { get; set; }

        public Guid OwnerId { get; set; }
        public ToDoUser Owner { get; set; }

        public ToDoItem()
        {
            Status = ToDoStatus.NotDone;
            Id = Guid.NewGuid();

        }

        public override bool Equals(object? obj)
        {
            var comp = obj as ToDoItem;

            if (comp == null)
            {
                return false;
            }

            return comp.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}