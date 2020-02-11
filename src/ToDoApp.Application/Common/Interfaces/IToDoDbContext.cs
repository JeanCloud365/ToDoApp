using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Common.Interfaces
{
    public interface IToDoDbContext
    {
        DbSet<ToDoItem> ToDoItems { get; set; }
        DbSet<ToDoUser> ToDoUsers { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}