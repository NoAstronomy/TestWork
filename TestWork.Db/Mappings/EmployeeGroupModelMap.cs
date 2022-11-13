using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TestWork.Db.Models;

namespace TestWork.Db.Mappings
{
    public class EmployeeGroupModelMap : IEntityTypeConfiguration<EmployeeGroupModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeGroupModel> builder)
        {
            builder.HasKey(x => new { x.EmployeeId, x.GroupId });

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.EmployeeId)
                .IsRequired();

            builder.ToTable("СотрудникиГруппы");
        }
    }
}