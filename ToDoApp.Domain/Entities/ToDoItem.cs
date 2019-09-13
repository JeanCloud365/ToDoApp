using System;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Domain.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public ToDoStatus Status { get; set; }
        
        public ToDoUser User { get; set; }

        public ToDoItem()
        {
            Status = ToDoStatus.NotDone;
            Id = Guid.NewGuid();

        }
        
        
        
        
    }
}