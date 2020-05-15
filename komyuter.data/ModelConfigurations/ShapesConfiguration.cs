using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class ShapesConfiguration : EntityTypeConfiguration<Shapes>, IModelConfiguration
    {
        public ShapesConfiguration()
        {
            this.ToTable("shapes");

            //SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.id);
            //this.HasKey(n => n.shape_pt_sequence);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.id).IsRequired();
            this.Property(n => n.shape_id).IsRequired();
            this.Property(n => n.shape_pt_lat).IsRequired();
            this.Property(n => n.shape_pt_lon).IsRequired();
            this.Property(n => n.shape_pt_sequence).IsRequired();

            this.Property(n => n.shape_dist_traveled).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.shape_id).HasMaxLength(30);

            //this.Property(n => n.shape_pt_lat).HasPrecision(5, 5);
            //this.Property(n => n.shape_pt_lon).HasPrecision(5, 5);

        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
