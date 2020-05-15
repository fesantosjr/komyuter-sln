﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

using komyuter.core.DomainClasses;
using komyuter.data.Interfaces;

namespace komyuter.data.ModelConfigurations
{
    public class NaviRTTripUpdatesConfiguration : EntityTypeConfiguration<NaviRTTripUpdates>, IModelConfiguration
    {

        public NaviRTTripUpdatesConfiguration()
        {
            this.ToTable("navi_rt_trip_updates");

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
            this.Property(n => n.rt_id).IsRequired();
            this.Property(n => n.trip_id).IsRequired();
            this.Property(n => n.route_id).IsRequired();
            this.Property(n => n.delay).IsRequired();
            this.Property(n => n.start_date).IsRequired();
            this.Property(n => n.start_time).IsRequired();
        }

        public void SetColumnLengths()
        {
            this.Property(n => n.trip_id).HasMaxLength(35);
            this.Property(n => n.route_id).HasMaxLength(35);
            this.Property(n => n.start_date).HasMaxLength(8);
            this.Property(n => n.start_time).HasMaxLength(8);
        }

        public void SetIndices()
        {
            //this.Property(w => w.agency_id).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_agency_agency_id")));
            //this.Property(w => w.LastUsed).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_NSBroker_LastUsed")));
        }

        #endregion
    }
}
