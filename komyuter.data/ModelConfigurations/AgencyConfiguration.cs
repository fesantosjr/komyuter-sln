using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class AgencyConfiguration : EntityTypeConfiguration<Agency>, IModelConfiguration
    {

        public AgencyConfiguration()
        {
            this.ToTable("agency");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.agency_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.agency_id).IsRequired();
            this.Property(n => n.agency_name).IsRequired();
            this.Property(n => n.agency_url).IsRequired();
            this.Property(n => n.agency_timezone).IsRequired();

            this.Property(n => n.agency_lang).IsOptional();
            this.Property(n => n.agency_phone).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.agency_id).HasMaxLength(11);
            this.Property(n => n.agency_name).HasMaxLength(255);
            this.Property(n => n.agency_url).HasMaxLength(255);
            this.Property(n => n.agency_timezone).HasMaxLength(50);
            this.Property(n => n.agency_lang).HasMaxLength(2);
            this.Property(n => n.agency_phone).HasMaxLength(50);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
