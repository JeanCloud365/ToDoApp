using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class ToDoStatusConfiguration : IEntityTypeConfiguration<ToDoStatus>
    {
        public void Configure(EntityTypeBuilder<ToDoStatus> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasData(ToDoStatus.Done);
            builder.HasData(ToDoStatus.NotDone);
            builder.ToTable("Status");
        }
    }
}