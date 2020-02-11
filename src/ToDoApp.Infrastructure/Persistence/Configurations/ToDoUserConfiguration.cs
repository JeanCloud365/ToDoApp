using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class ToDoUserConfiguration : IEntityTypeConfiguration<ToDoUser>
    {
        public void Configure(EntityTypeBuilder<ToDoUser> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Items).WithOne(o => o.Owner).HasForeignKey(o => o.OwnerId);
            builder.HasMany(o => o.Webhooks).WithOne(o => o.Owner).HasForeignKey(o => o.OwnerId);
            builder.ToTable("Users");
        }
    }
}