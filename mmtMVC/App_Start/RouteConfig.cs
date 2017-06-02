using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using mmtMVC.Controllers;

namespace mmtMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "EditSection",
             url: "{controller}/{action}/{eid}/{secid}",
             defaults: new { controller = "Sections", action = "Edit", eid = "", secid = "" }
             );

            routes.MapRoute(
              name: "SectionListing",
              url: "{controller}/{action}/{eid}",
              defaults: new { controller = "Sections", action = "Listing", eid = "" }
              );








            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );

            // Parameter defaults )
            //routes.MapRoute(
            //          name: "EditSections",
            //         url: "Sections/Edit/{examid}/{secid}",
            //        defaults: new { controller = "Sections", action = "Edit", examid = 0, secid = 0 }
            //  );


        }
    }
}
