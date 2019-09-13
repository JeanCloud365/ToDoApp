using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enumerations;

namespace ToDoApp.Persistence.Configurations
{
    public class ToDoItemConfiguration:IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            //builder.Property(o => o.Id).HasValueGenerator<GuidValueGenerator>();
            builder.HasOne<ToDoUser>(o => o.User).WithMany();
            builder.OwnsOne<ToDoStatus>(o => o.Status).WithOwner();
            builder.ToTable("Items");
        }
    }
}