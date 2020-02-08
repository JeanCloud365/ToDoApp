using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            //builder.OwnsOne(o => o.Status).WithOwner();
            builder.ToTable("Items");
        }
    }
}