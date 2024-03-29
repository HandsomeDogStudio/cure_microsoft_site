﻿using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace ProjectCure.Web
{
    public class BundleConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyles(bundles);
            RegisterScripts(bundles);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void RegisterStyles(BundleCollection bundles)
        {
            //CSS Styles
            bundles.Add(new StyleBundle(StyleBundles.Site).Include(
                        "~/Content/css/bootstrap.css",
                        "~/Content/css/bootstrap-custom.css",
                        "~/Content/css/bootstrap-theme.css",
                        "~/Content/css/fullcalendar.css",
                        "~/Content/css/fullcalendar.print.css",
                        "~/Content/css/site.css").ForceOrdered());
        }

        /// <summary>
        /// 
        /// </summary>
        private static void RegisterScripts(BundleCollection bundles)
        {
            //Mod
            bundles.Add(new ScriptBundle(ScriptBundles.Modernizr).Include(
                        "~/Scripts/core/modernizr-{version}.js"));
            //Jquery
            bundles.Add(new ScriptBundle(ScriptBundles.Respond).Include(
                        "~/Scripts/core/respond.js",
                        "~/Scripts/core/respond.matchmedia.addListener.js"));

            //RespondJs (for IE8)
            bundles.Add(new ScriptBundle(ScriptBundles.Jquery).Include(
            "~/Scripts/jquery/jquery-{version}.js",
            "~/Scripts/jquery/jquery-migrate-{version}.js"));

            //Bootstrap
            bundles.Add(new ScriptBundle(ScriptBundles.Bootstrap).Include(
                        "~/Scripts/bootstrap/bootstrap.js"));

            //jquery custom plugins
            bundles.Add(new ScriptBundle(ScriptBundles.JqueryPlugins).Include(
                "~/Scripts/jquery/plugins/moment.js",
                "~/Scripts/jquery/plugins/jquery.form.js",
                "~/Scripts/jquery/plugins/jquery.validate.js",
                "~/Scripts/jquery/plugins/additional-methods.js",
                "~/Scripts/jquery/plugins/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery/plugins/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery/plugins/fullcalendar.js",
                "~/Scripts/jquery/plugins/jquery.maskedinput.js",
                "~/Scripts/jquery/plugins/gcal.js").ForceOrdered());

            //jquery custom
            bundles.Add(new ScriptBundle(ScriptBundles.Custom).Include(
                "~/Scripts/custom/main.js"));

            //Calendar
            bundles.Add(new ScriptBundle(ScriptBundles.Calendar).Include(
                "~/Scripts/custom/calendar.js"));
        }
    }

    public class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
        {
            return files;
        }
    }

    internal static class BundleExtensions
    {
        public static Bundle ForceOrdered(this Bundle sb)
        {
            sb.Orderer = new AsIsBundleOrderer();
            return sb;
        }
    }
}