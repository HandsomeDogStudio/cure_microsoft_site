using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectCureData.Models.Mapping
{
    public class TemplateMap : EntityTypeConfiguration<Template>
    {
        public TemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.TemplateId);

            // Properties
            this.Property(t => t.TemplateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TemplateName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TemplateText)
                .IsRequired();

            this.Property(t => t.TemplateSubject)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Templates");
            this.Property(t => t.TemplateId).HasColumnName("TemplateId");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.TemplateText).HasColumnName("TemplateText");
            this.Property(t => t.TemplateSubject).HasColumnName("TemplateSubject");
        }
    }
}
