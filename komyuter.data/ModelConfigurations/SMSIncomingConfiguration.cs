using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class SMSIncomingConfiguration : EntityTypeConfiguration<SMSIncoming>, IModelConfiguration
    {
        public SMSIncomingConfiguration()
        {
            this.ToTable("sms_incoming");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.sms_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.sms_id).IsRequired();
            this.Property(n => n.message).IsRequired();

            this.Property(n => n.process_date).IsOptional();
            this.Property(n => n.process_remarks).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.message).HasMaxLength(500);
            this.Property(n => n.process_remarks).HasMaxLength(500);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
