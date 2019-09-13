using System.Collections.Generic;

namespace ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers
{
    public class ListToDoUsersViewModel
    {
        public IEnumerable<ListToDoUsersDto> Items { get; set; }
    }
}