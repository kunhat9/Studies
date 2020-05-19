using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Login",
            //    url: "Login/{id}",
            //    defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Logout",
            //    url: "Logout/{id}",
            //    defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "KhuyenMai",
            //    url: "KhuyenMai/{action}/{id}",
            //    defaults: new { controller = "KhuyenMai", action = "DanhSach", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "KhachHang",
            //    url: "KhachHang/{action}/{id}",
            //    defaults: new { controller = "KhachHang", action = "DanhSach", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "GoiTap",
            //    url: "GoiTap/{action}/{id}",
            //    defaults: new { controller = "GoiTap", action = "DanhSach", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "KhoHang",
            //    url: "KhoHang/{action}/{id}",
            //    defaults: new { controller = "KhoHang", action = "DanhSach", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "ChiPhi",
            //    url: "ChiPhi/{action}/{id}",
            //    defaults: new { controller = "ChiPhi", action = "DanhSach", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
