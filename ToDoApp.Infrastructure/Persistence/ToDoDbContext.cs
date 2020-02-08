using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Common.Interfaces;
using ToDoApp.Domain.Common;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Persistence
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory = 
            new LoggerFactory(new[] { 
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() 
            });
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTime;

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options,ICurrentUserService currentUserService,IDateTimeService dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }


        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<ToDoUser> ToDoUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Id.ToString();
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.Id.ToString();
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
        }


    }
}