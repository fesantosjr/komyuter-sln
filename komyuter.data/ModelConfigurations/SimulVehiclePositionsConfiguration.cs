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
    public class SimulVehiclePositionsConfiguration : EntityTypeConfiguration<SimulVehiclePositions>, IModelConfiguration
    {
        public SimulVehiclePositionsConfiguration()
        {
            this.ToTable("simul_vehicle_positions");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.route_id).HasMaxLength(35);
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
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.direction_id).IsRequired();
            this.Property(n => n.arrival_time).IsRequired();
            this.Property(n => n.pos_lat).IsRequired();
            this.Property(n => n.pos_lon).IsRequired();
        }
    }
}
