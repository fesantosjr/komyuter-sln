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
    public class RTServiceAlertsConfiguration : EntityTypeConfiguration<RTServiceAlerts>, IModelConfiguration
    {
        public RTServiceAlertsConfiguration()
        {
            this.ToTable("rt_service_alerts");

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
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.start_date).IsRequired();
            this.Property(n => n.header).IsRequired();
            this.Property(n => n.description).IsRequired();

            this.Property(n => n.stop_id).IsOptional();
            this.Property(n => n.end_date).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.route_id).HasMaxLength(20);
            this.Property(n => n.stop_id).HasMaxLength(20);
            this.Property(n => n.header).HasMaxLength(255);
            this.Property(n => n.description).HasMaxLength(500);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
