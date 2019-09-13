using System;

namespace ToDoApp.Application.Interfaces
{
    public interface ICurrentUser
    {
        Guid Id { get; }
        
        string Mail { get; }
        
        string Name { get; }
    }
}