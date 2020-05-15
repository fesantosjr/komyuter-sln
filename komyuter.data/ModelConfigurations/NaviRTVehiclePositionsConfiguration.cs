using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace komyuter.data.ModelConfigurations
{
    public class NaviRTVehiclePositionsConfiguration : EntityTypeConfiguration<NaviRTVehiclePositions>, IModelConfiguration
    {
        public NaviRTVehiclePositionsConfiguration()
        {
            this.ToTable("navi_rt_vehicle_positions");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.route_id).HasMaxLength(35);
            this.Property(n => n.trip_id).HasMaxLength(35);
            this.Property(n => n.stop_id).HasMaxLength(35);
            this.Property(n => n.vehicle_id).HasMaxLength(35);
            this.Property(n => n.vehicle_label).HasMaxLength(35);
            this.Property(n => n.vehicle_license_plate).HasMaxLength(35);


            this.Property(n => n.start_date).HasMaxLength(8);
            this.Property(n => n.start_time).HasMaxLength(8);
        }

        public void SetForeignKeys()
        {
        }

        public void SetIndices()
        {
        }

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.id);
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.id).IsRequired();
            this.Property(n => n.rt_id).IsRequired();
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.trip_id).IsRequired();
            this.Property(n => n.direction_id).IsRequired();
            this.Property(n => n.start_date).IsRequired();
            this.Property(n => n.start_time).IsRequired();
            this.Property(n => n.latitude).IsRequired();
            this.Property(n => n.longitude).IsRequired();
            this.Property(n => n.timestamp).IsRequired();

            this.Property(n => n.stop_id).IsOptional();
            this.Property(n => n.current_stop_sequence).IsOptional();
            this.Property(n => n.vehicle_id).IsOptional();
            this.Property(n => n.vehicle_label).IsOptional();
            this.Property(n => n.vehicle_license_plate).IsOptional();
        }
    }
}
