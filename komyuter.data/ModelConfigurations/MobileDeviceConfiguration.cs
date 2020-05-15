using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class MobileDeviceConfiguration : EntityTypeConfiguration<MobileDevice>, IModelConfiguration
    {
        public MobileDeviceConfiguration()
        {
            this.ToTable("mobile_device");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.mobile_device_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.mobile_device_id).IsRequired();
            this.Property(n => n.mobile_number).IsRequired();
            this.Property(n => n.access_token).IsRequired();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.mobile_number).HasMaxLength(25);
            this.Property(n => n.access_token).HasMaxLength(255);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
