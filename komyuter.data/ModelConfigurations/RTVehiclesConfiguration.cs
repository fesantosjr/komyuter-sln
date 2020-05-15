using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class RTVehiclesConfiguration : EntityTypeConfiguration<RTVehicles>, IModelConfiguration
    {

        public RTVehiclesConfiguration()
        {
            this.ToTable("rt_vehicles");

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
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.start_date).IsRequired();
            this.Property(n => n.timestamp).IsRequired();

            this.Property(n => n.vehicle_lat).IsOptional();
            this.Property(n => n.vehicle_lon).IsOptional();
            this.Property(n => n.stop_id).IsOptional();
            this.Property(n => n.vehicle_id).IsOptional();
            this.Property(n => n.vehicle_label).IsOptional();
        }

        public void SetColumnLengths()
        {
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
