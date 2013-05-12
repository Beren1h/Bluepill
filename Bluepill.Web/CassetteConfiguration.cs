using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Bluepill.Web
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            // TODO: Configure your bundles here...
            // Please read http://getcassette.net/documentation/configuration

            // This default configuration treats each file as a separate 'bundle'.
            // In production the content will be minified, but the files are not combined.
            // So you probably want to tweak these defaults!
            //bundles.AddPerIndividualFile<StylesheetBundle>("Content");
            //bundles.AddPerIndividualFile<ScriptBundle>("Scripts");
            //bundles.Add("/content/bootstrap.css");
            //bundles.Add("/content/bootstrap-responsive.css");
            //bundles.Add("/content/page.css");
            //bundles.Add("/content/application.search.interface.css");

            //bundles.Add<StylesheetBundle>("/content/bootstrap.css");
            //bundles.Add<StylesheetBundle>("/content/bootstrap-responsive.css");
            //bundles.Add<StylesheetBundle>("/content/page.css");
            //bundles.Add<StylesheetBundle>("/content/application.search.interface.css");

            var layoutFiles = new[]{
                "content/bootstrap.css",
                "content/bootstrap-responsive.css",
                "content/page.css",
                "content/application.search.interface.css",
                "content/bluepill.create.css",
                "content/bluepill.search.css"
            };

            bundles.Add<StylesheetBundle>("layout_css", layoutFiles);


            //bundles.AddPerSubDirectory<StylesheetBundle>("content");


    //<link href="/content/bootstrap.css" rel="stylesheet" type="text/css" />
    //<link href="/content/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    //<link href="/content/page.css" rel="stylesheet" type="text/css" />
    //<link href="/content/application.search.interface.css" rel="stylesheet" type="text/css" />



            // To combine files, try something like this instead:
            //   bundles.Add<StylesheetBundle>("Content");
            // In production mode, all of ~/Content will be combined into a single bundle.
            
            // If you want a bundle per folder, try this:
            //   bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
            // Each immediate sub-directory of ~/Scripts will be combined into its own bundle.
            // This is useful when there are lots of scripts for different areas of the website.
        }
    }
}