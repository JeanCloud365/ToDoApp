using System;

namespace ToDoApp.Domain.Entities
{
    public class ToDoUser
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Mail { get; set; }
    }
}