using System.Collections.Generic;

namespace ToDoApp.Application.ToDoItems.Queries.ListToDoItems
{
    public class ListToDoItemsViewModel
    {
        public IEnumerable<ListToDoItemsDto> Items { get; set; }
    }
}