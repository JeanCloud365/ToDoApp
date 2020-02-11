using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Application.ToDoItems.Queries.GetToDoItem
{
    public class GetToDoItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
