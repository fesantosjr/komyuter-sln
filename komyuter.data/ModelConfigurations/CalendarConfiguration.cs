using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class CalendarConfiguration : EntityTypeConfiguration<Calendar>, IModelConfiguration
    {

        public CalendarConfiguration()
        {
            this.ToTable("calendar");

            SetPrimaryKeys();
            SetForeignKeys();
            SetRequiredFields();
            SetColumnLengths();
            SetIndices();
        }

        #region IModelConfiguration Members

        public void SetPrimaryKeys()
        {
            this.HasKey(n => n.service_id);
        }

        public void SetForeignKeys()
        {
            return;
        }

        public void SetRequiredFields()
        {
            this.Property(n => n.service_id).IsRequired();
            this.Property(n => n.monday).IsRequired();
            this.Property(n => n.tuesday).IsRequired();
            this.Property(n => n.wednesday).IsRequired();
            this.Property(n => n.thursday).IsRequired();
            this.Property(n => n.friday).IsRequired();
            this.Property(n => n.saturday).IsRequired();
            this.Property(n => n.sunday).IsRequired();
            this.Property(n => n.start_date).IsRequired();
            this.Property(n => n.end_date).IsRequired();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.service_id).HasMaxLength(15);
            this.Property(n => n.start_date).HasMaxLength(8);
            this.Property(n => n.end_date).HasMaxLength(8);
        }


        //  [service_id][varchar](15) NOT NULL,

        // [monday] [bit] NULL,
        //[tuesday] [bit] NULL,
        //[wednesday] [bit] NULL,
        //[thursday] [bit] NULL,
        //[friday] [bit] NULL,
        //[saturday] [bit] NULL,
        //[sunday] [bit] NULL,
        //[start_date] [varchar] (8) NULL,
        //[end_date] [varchar] (8) NULL,

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
