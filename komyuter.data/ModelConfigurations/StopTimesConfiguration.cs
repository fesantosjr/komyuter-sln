using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class StopTimesConfiguration : EntityTypeConfiguration<StopTimes>, IModelConfiguration
    {
        public StopTimesConfiguration()
        {
            this.ToTable("stop_times");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.id).IsRequired();
            this.Property(n => n.trip_id).IsRequired();
            this.Property(n => n.stop_id).IsRequired();
            this.Property(n => n.stop_sequence).IsRequired();

            this.Property(n => n.stop_headsign).IsOptional();
            this.Property(n => n.pickup_type).IsOptional();
            this.Property(n => n.drop_off_type).IsOptional();
            this.Property(n => n.shape_dist_traveled).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.trip_id).HasMaxLength(35);
            this.Property(n => n.stop_id).HasMaxLength(35);
            this.Property(n => n.stop_headsign).HasMaxLength(50);
            this.Property(n => n.shape_dist_traveled).HasMaxLength(50);
            this.Property(n => n.stop_id).HasMaxLength(20);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
