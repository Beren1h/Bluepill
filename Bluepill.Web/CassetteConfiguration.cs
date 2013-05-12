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

            var cssFiles = new[]{
                "content/bootstrap.css",
                "content/bootstrap-responsive.css",
                "content/page.css",
                "content/application.search.interface.css",
                "content/bluepill.create.css",
                "content/bluepill.search.css"
            };

            var jsFiles = new[]{
                //"scripts/jquery-1.9.1.intellisense.js",
                "scripts/jquery-1.9.1.js",
                "scripts/jquery-ui-1.10.2.js",
                "scripts/bootstrap.js",
                "scripts/navbar.js",
                "scripts/application.search.interface.js",
                //"scripts/bluepill.create.js",
                //"scripts/bluepill.search.js",
                //"scripts/bluepill.search.mobile.js",
                //"scripts/jquery.mousewheel.js",
                //"scripts/swipe.js"
            };

            var createJsFiles = new[]{
                "scripts/bluepill.create.js"
            };


            var searchJsFiles = new[]{
                "scripts/bluepill.search.js",
                "scripts/bluepill.search.mobile.js",
                "scripts/jquery.mousewheel.js",
                "scripts/swipe.js"
            };


            bundles.Add<StylesheetBundle>("css", cssFiles);
            bundles.Add<ScriptBundle>("js", jsFiles);
            bundles.Add<ScriptBundle>("createJs", createJsFiles);
            bundles.Add<ScriptBundle>("searchJs", searchJsFiles);


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