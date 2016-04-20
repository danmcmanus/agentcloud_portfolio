
using Backload.Bundles;
using System.Web;
using System.Web.Optimization;

namespace Insure.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string vendor = "blueimp";
            string plugin = "fileupload";
            string jsroot = "~/Backload/Client";
            string cssroot = "~/Backload/Client";
            string jsvendor = string.Empty;
            string cssvendor = string.Empty;
            string jsplugin = string.Empty;
            string cssplugin = string.Empty;
            try
            {
                Backload.Configuration.Bundles bundle = new Backload.Configuration.Bundles();
                jsroot = Backload.Configuration.Bundles.ClientScripts;
                cssroot = Backload.Configuration.Bundles.ClientStyles;
            }
            catch
            {
            }
            jsvendor = string.Format("{0}/{1}/", jsroot, vendor);
            jsplugin = string.Format("{0}{1}/js/", jsvendor, plugin);
            cssvendor = string.Format("{0}/{1}/", cssroot, vendor);
            cssplugin = string.Format("{0}{1}/css/", cssvendor, plugin);

            string[] scripts = new string[] {
                jsvendor + "templates/js/tmpl.min.js",
                jsvendor + "loadimage/js/load-image.all.min.js",
                jsvendor + "blob/js/canvas-to-blob.min.js",
                jsvendor + "gallery/js/jquery.blueimp-gallery.min.js",
                jsplugin + "vendor/jquery.ui.widget.js",
                jsplugin + "jquery.iframe-transport.js",
                jsplugin + "jquery.fileupload.js",
                jsplugin + "jquery.fileupload-process.js",
                jsplugin + "jquery.fileupload-image.js",
                jsplugin + "jquery.fileupload-audio.js",
                jsplugin + "jquery.fileupload-video.js",
                jsplugin + "jquery.fileupload-validate.js",
                jsplugin + "jquery.fileupload-ui.js",
                jsplugin + "themes/jquery.fileupload-themes.js" };

            string[] styles = new string[] {
                cssvendor + "gallery/css/blueimp-gallery.min.css",
                cssplugin + "jquery.fileupload.css",
                cssplugin + "jquery.fileupload-ui.css" };


            bundles.Add(new ScriptBundle("~/backload/blueimp/bootstrap/BasicPlusUI").Include(scripts));
            bundles.Add(new StyleBundle("~/backload/blueimp/bootstrap/BasicPlusUI/css").Include(styles));

            


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
