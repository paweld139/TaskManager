using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace TaskManager.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                //"~/Scripts/PDWebCore/jquery.enhsplitter.js",
                "~/Scripts/PDWebCore/jquery.card.js"));

            bundles.Add(new ScriptBundle("~/bundles/tooltip").Include(
               "~/Scripts/PDWebCore/tether.min.js",
               "~/Scripts/PDWebCore/drop.min.js",
               "~/Scripts/PDWebCore/tooltip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                //"~/Scripts/PDWebCore/knockout-sortable.js",
                "~/Scripts/knockout.validation.js"));

            //bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
            //    "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/Index").Include(
                "~/Scripts/PDWebCore/Objects.js",
                "~/Scripts/PDWebCore/Tools.js",
                "~/Scripts/PDWebCore/cookies.js",
                "~/Scripts/PDWebCore/SortUtil.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/PDWebCore/datepicker-pl.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/add.viewmodel.js",
                "~/Scripts/app/details.viewmodel.js",
                "~/Scripts/app/reports.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/umd/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-select.js"
                /*"~/Scripts/PDWebCore/bootstrap-select.js",*/
                /*"~/Content/PDWebCore/fontawesome/js/all.js"*/));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-select.css",
                "~/Content/themes/base/jquery-ui.css").Include(
                "~/Content/PDWebCore/fontawesome/css/all.css", new CssRewriteUrlTransform()).Include(
                "~/Content/flag-icon-css/css/flag-icon.css", new CssRewriteUrlTransform()).Include(
                /*"~/Content/PDWebCore/bootstrap-select.css",
                "~/Content/PDWebCore/Main.css",
                "~/Content/PDWebCore/webmail.css",
                "~/Content/PDWebCore/jquery.enhsplitter.css",
                "~/Content/jquery-ui.css",*/));

            bundles.Add(new StyleBundle("~/Content/customCss").Include(
                    "~/Content/PDWebCore/tooltip-theme-arrows.min.css",
                    "~/Content/PDWebCore/tooltip.css",
                    "~/Content/Site.css"
                ));
        }
    }
}
