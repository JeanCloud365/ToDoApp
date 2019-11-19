using System;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid Id { get; }
        
        string Mail { get; }
        
        string Name { get; }
    }
}