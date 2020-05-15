using System.Collections.Generic;
using System.Data.Entity;
using komyuter.core.DomainClasses;

namespace komyuter.data.Migrations
{
    public class KomyuterContextInitializer : CreateDatabaseIfNotExists<KomyuterContext>
    {
        protected override void Seed(KomyuterContext context)
        {
            /* Seed the agency table */
            SeedAgencyTable(context);

            base.Seed(context);
            context.SaveChanges();
        }

        protected void SeedAgencyTable(KomyuterContext context)
        {
            new List<Agency>
            {
                new Agency { agency_id = "agency_id", agency_name = "agency_name", agency_url = "agency_url", agency_timezone = "", agency_lang = "en"}
            }.ForEach(a => context.Agency.Add(a));
        }
    }
}
