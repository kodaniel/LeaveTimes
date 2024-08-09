using LeaveTimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveTimes.Infrastructure.EntityConfigs;

internal class LeaveTimeTypeConfiguration : IEntityTypeConfiguration<LeaveTime>
{
    public void Configure(EntityTypeBuilder<LeaveTime> builder)
    {
        builder.ToTable("leave-times");

        builder.Property(x => x.EmployeeName)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasConversion(
                v => v.ToString(),
                v => (Reason)Enum.Parse(typeof(Reason), v));

        // More SQL datatable settings come here...
    }
}
