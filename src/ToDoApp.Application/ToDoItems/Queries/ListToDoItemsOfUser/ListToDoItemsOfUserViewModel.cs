using System.Collections.Generic;

namespace ToDoApp.Application.ToDoItems.Queries.ListToDoItemsOfUser
{
    public class ListToDoItemsOfUserViewModel
    {
        public IEnumerable<ListToDoItemsOfUserDto> Items { get; set; }
    }
}