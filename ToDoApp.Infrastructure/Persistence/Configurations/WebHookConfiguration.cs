using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebHook = ToDoApp.Domain.Entities.WebHook;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class WebHookConfiguration : IEntityTypeConfiguration<WebHook>
    {
        public void Configure(EntityTypeBuilder<WebHook> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedNever();
            builder.ToTable("Webhooks");
        }

    }

}
