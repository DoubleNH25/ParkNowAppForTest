using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.FindingSlotManagement.Domain.Entities;

namespace Parking.FindingSlotManagement.Infrastructure.Persistences.Configuration
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.Property(x => x.NumberOfParking).HasMaxLength(20);
        }
    }
}