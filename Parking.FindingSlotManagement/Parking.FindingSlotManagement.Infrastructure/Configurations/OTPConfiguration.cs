using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Infrastructure.Configurations
{
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.HasKey(x => x.OTPId);
            
            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);
                
            builder.Property(x => x.ExpirationTime)
                .IsRequired();
                
            builder.Property(x => x.CreatedDate)
                .IsRequired();
                
            builder.Property(x => x.IsUsed)
                .IsRequired()
                .HasDefaultValue(false);
                
            builder.Property(x => x.UserId)
                .IsRequired();

            // No foreign key constraint to allow registration OTPs with UserId = -1
            // builder.HasOne(x => x.User)
            //     .WithMany()
            //     .HasForeignKey(x => x.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
