using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using komyuter.core.DomainClasses;
using komyuter.data.ModelConfigurations;

namespace komyuter.data
{
    public class KomyuterContext : DbContext
    {
        public KomyuterContext() : base("KomyuterContext") { }

        public DbSet<Agency> Agency { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<Shapes> Shapes { get; set; }
        public DbSet<Stops> Stops { get; set; }
        public DbSet<StopTimes> StopTimes { get; set; }
        public DbSet<Frequencies> Frequencies { get; set; }
        public DbSet<Trips> Trips { get; set; }

        public DbSet<SMSIncoming> SMSIncomings { get; set; }
        public DbSet<MobileDevice> MobileDevices { get; set; }

        public DbSet<RTServiceAlerts> RTServiceAlerts { get; set; }
        public DbSet<RTTripUpdates> RTTripUpdates { get; set; }
        public DbSet<RTVehiclePositions> RTVehiclePositions { get; set; }

        public DbSet<NaviRTServiceAlerts> NaviRTServiceAlerts { get; set; }
        public DbSet<NaviRTTripUpdates> NaviRTTripUpdates { get; set; }
        public DbSet<NaviRTVehiclePositions> NaviRTVehiclePositions { get; set; }

        public DbSet<SimulVehiclePositions> SimulVehiclePositions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new AgencyConfiguration());
            modelBuilder.Configurations.Add(new CalendarConfiguration());
            modelBuilder.Configurations.Add(new RoutesConfiguration());
            modelBuilder.Configurations.Add(new ShapesConfiguration());
            modelBuilder.Configurations.Add(new StopsConfiguration());
            modelBuilder.Configurations.Add(new StopTimesConfiguration());
            modelBuilder.Configurations.Add(new TripsConfiguration());
            modelBuilder.Configurations.Add(new FrequenciesConfiguration());

            modelBuilder.Configurations.Add(new MobileDeviceConfiguration());
            modelBuilder.Configurations.Add(new SMSIncomingConfiguration());
            modelBuilder.Configurations.Add(new LBSLogConfiguration());

            modelBuilder.Configurations.Add(new RTTripUpdatesConfiguration());
            modelBuilder.Configurations.Add(new RTServiceAlertsConfiguration());
            modelBuilder.Configurations.Add(new RTVehiclePositionsConfiguration());

            modelBuilder.Configurations.Add(new NaviRTServiceAlertsConfiguration());
            modelBuilder.Configurations.Add(new NaviRTTripUpdatesConfiguration());
            modelBuilder.Configurations.Add(new NaviRTVehiclePositionsConfiguration());

            modelBuilder.Configurations.Add(new SimulVehiclePositionsConfiguration());
        }
    }
}
