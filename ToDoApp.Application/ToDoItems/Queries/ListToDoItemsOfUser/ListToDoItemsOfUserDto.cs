using System;

namespace ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser
{
    public class ListToDoItemsOfUserDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}