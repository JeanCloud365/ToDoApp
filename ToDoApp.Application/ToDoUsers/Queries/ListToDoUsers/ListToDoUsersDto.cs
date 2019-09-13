using System;

namespace ToDoApp.Application.ToDoUsers.Queries.ListToDoUsers
{
    public class ListToDoUsersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
    }
}