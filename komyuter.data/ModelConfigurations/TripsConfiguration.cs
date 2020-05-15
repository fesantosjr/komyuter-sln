using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class TripsConfiguration : EntityTypeConfiguration<Trips>, IModelConfiguration
    {

        public TripsConfiguration()
        {
            this.ToTable("trips");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.trip_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.service_id).IsRequired();
            this.Property(n => n.shape_id).IsRequired();
            this.Property(n => n.trip_id).IsRequired();

            this.Property(n => n.bikes_allowed).IsOptional();
            this.Property(n => n.block_id).IsOptional();
            this.Property(n => n.direction_id).IsOptional();
            this.Property(n => n.trip_headsign).IsOptional();
            this.Property(n => n.trip_short_name).IsOptional();
            this.Property(n => n.wheelchair_accessible).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.route_id).HasMaxLength(35);
            this.Property(n => n.service_id).HasMaxLength(35);
            this.Property(n => n.shape_id).HasMaxLength(35);
            this.Property(n => n.trip_id).HasMaxLength(35);

            this.Property(n => n.trip_headsign).HasMaxLength(255);
            this.Property(n => n.trip_short_name).HasMaxLength(50);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
