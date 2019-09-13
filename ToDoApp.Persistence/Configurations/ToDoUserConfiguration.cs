using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Persistence.Configurations
{
    public class ToDoUserConfiguration:IEntityTypeConfiguration<ToDoUser>
    {
        public void Configure(EntityTypeBuilder<ToDoUser> builder)
        {
            //builder.Property(o => o.Id).HasValueGenerator<GuidValueGenerator>();
            builder.ToTable("Users");
        }
    }
}