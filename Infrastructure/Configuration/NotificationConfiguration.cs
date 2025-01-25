using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(n => n.Id);
            builder.HasDiscriminator<string>("NotificationType")
            .HasValue<SMSNotification>("SMS")
            .HasValue<EmailNotification>("Email")
            .HasValue<RealTimeNotification>("RealTime");
            builder.HasIndex(n => n.CreatedDate);
        }
    }
}
