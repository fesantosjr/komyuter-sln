using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class StopsConfiguration : EntityTypeConfiguration<Stops>, IModelConfiguration
    {
        public StopsConfiguration()
        {
            this.ToTable("stops");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.stop_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.stop_id).IsRequired();

            this.Property(n => n.stop_code).IsOptional();
            this.Property(n => n.stop_desc).IsOptional();
            this.Property(n => n.zone_id).IsOptional();
            this.Property(n => n.stop_url).IsOptional();
            this.Property(n => n.location_type).IsOptional();
            this.Property(n => n.parent_station).IsOptional();
            this.Property(n => n.wheelchair_boarding).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.stop_id).HasMaxLength(20);
            this.Property(n => n.stop_code).HasMaxLength(50);
            this.Property(n => n.stop_name).HasMaxLength(255);
            this.Property(n => n.stop_desc).HasMaxLength(255);
            this.Property(n => n.stop_url).HasMaxLength(255);

            //this.Property(n => n.stop_lat).HasPrecision(9, 5);
            //this.Property(n => n.stop_lon).HasPrecision(9, 5);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
