using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class FrequenciesConfiguration : EntityTypeConfiguration<Frequencies>, IModelConfiguration
    {
        public FrequenciesConfiguration()
        {
            this.ToTable("frequencies");

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
            this.Property(n => n.start_time).IsRequired();
            this.Property(n => n.end_time).IsRequired();
            this.Property(n => n.headway_secs).IsRequired();

            this.Property(n => n.exact_times).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.trip_id).HasMaxLength(35);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
