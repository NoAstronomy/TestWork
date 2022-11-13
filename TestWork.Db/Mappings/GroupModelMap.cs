using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestWork.Db.Models;

namespace TestWork.Db.Mappings
{
    public class GroupModelMap : IEntityTypeConfiguration<GroupModel>
    {
        public void Configure(EntityTypeBuilder<GroupModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasData(
                new GroupModel("Администраторы"), 
                new GroupModel("Руководство"));

            builder.ToTable("Группы");
        }
    }
}