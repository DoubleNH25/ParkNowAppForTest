using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Infrastructure.Persistences.Configuration
{
    public class TrafficConfiguration : IEntityTypeConfiguration<Traffic>
    {
        public void Configure(EntityTypeBuilder<Traffic> builder)
        {
            builder.ToTable("VehicleType");

            builder.Property(e => e.Name).HasMaxLength(50);
        }
    }
}
