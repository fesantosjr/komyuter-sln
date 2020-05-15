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
    public class RTTripUpdatesStopTimesConfiguration : EntityTypeConfiguration<RTTripUpdatesStopTimes>, IModelConfiguration
    {

        public RTTripUpdatesStopTimesConfiguration()
        {
            this.ToTable("rt_trip_updates_stoptimes");

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
            this.Property(n => n.rt_trip_update_id).IsRequired();
            this.Property(n => n.stop_id).IsRequired();

            this.Property(n => n.arrival_delay).IsOptional();
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
