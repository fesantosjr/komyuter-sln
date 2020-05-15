using System.Web;
using System.Web.Optimization;

namespace komyuter.web_navi
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

           // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/js/komyuter").Include(
                      "~/Scripts/leaflet.js",
                      "~/Scripts/Leaflet.label.js",
                      "~/js/moment.min.js",
                      "~/js/settings.js"));

            bundles.Add(new ScriptBundle("~/js/leaflet.label").Include(
                      "~/Scripts/Label.js",
                      "~/Scripts/BaseMarkerMethods.js",
                      "~/Scripts/Marker.Label.js",
                      "~/Scripts/CircleMarker.Label.js",
                      "~/Scripts/Path.Label.js",
                      "~/Scripts/Map.Label.js",
                      "~/Scripts/FeatureGroup.Label.js"));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                        "~/js/komyuter-leaf.js",
                        "~/js/app.js"));


            bundles.Add(new StyleBundle("~/css/komyuter").Include(
                        "~/css/leaflet.css",
                        "~/css/leaflet.label.css",
                      "~/css/light.css"));

        }
    }
}
