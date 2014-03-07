using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectCureData.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserEmail)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.UserFirstName)
                .HasMaxLength(50);

            this.Property(t => t.UserLastName)
                .HasMaxLength(50);

            this.Property(t => t.UserPassword)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserEmail).HasColumnName("UserEmail");
            this.Property(t => t.UserFirstName).HasColumnName("UserFirstName");
            this.Property(t => t.UserLastName).HasColumnName("UserLastName");
            this.Property(t => t.UserRoleId).HasColumnName("UserRoleId");
            this.Property(t => t.UserActiveIn).HasColumnName("UserActiveIn");
            this.Property(t => t.UserNotifyFiveDays).HasColumnName("UserNotifyFiveDays");
            this.Property(t => t.UserNotifyTenDays).HasColumnName("UserNotifyTenDays");
            this.Property(t => t.UserPassword).HasColumnName("UserPassword");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserRoleId);

        }
    }
}
