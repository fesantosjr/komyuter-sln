using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class RoutesConfiguration : EntityTypeConfiguration<Routes>, IModelConfiguration
    {

        public RoutesConfiguration()
        {
            this.ToTable("routes");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.route_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.route_type).IsRequired();

            this.Property(n => n.route_text_color).IsOptional();
            this.Property(n => n.default_zoom).IsOptional();
            this.Property(n => n.center_lat).IsOptional();
            this.Property(n => n.center_lon).IsOptional();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.route_id).HasMaxLength(15);
            this.Property(n => n.agency_id).HasMaxLength(11);
            this.Property(n => n.route_short_name).HasMaxLength(50);
            this.Property(n => n.route_long_name).HasMaxLength(255);
            this.Property(n => n.route_text_color).HasMaxLength(255);
            this.Property(n => n.route_color).HasMaxLength(255);
            this.Property(n => n.route_url).HasMaxLength(255);
            this.Property(n => n.route_desc).HasMaxLength(255);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
