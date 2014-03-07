using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectCureData.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.EventId);

            // Properties
            this.Property(t => t.EventTitle)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.EventId).HasColumnName("EventId");
            this.Property(t => t.EventTitle).HasColumnName("EventTitle");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.EventStart).HasColumnName("EventStart");
            this.Property(t => t.EventEnd).HasColumnName("EventEnd");
            this.Property(t => t.EventStatus).HasColumnName("EventStatus");
            this.Property(t => t.EventManagerId).HasColumnName("EventManagerId");
            this.Property(t => t.EventDescription).HasColumnName("EventDescription");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.EventManagerId);

        }
    }
}
