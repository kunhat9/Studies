using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.AppSession;

namespace WebAdmin.Configuration
{
    public class AuthorizeBusiness : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //List<string> notCheck = new List<string> {
            //    "/Home/Login",
            //    "/Home/Logout",
            //    "/Home/_HomeHeader",
            //    "/Home/_HomeFooter",
            //    "/Home/_HomeMenuLeft",
            //    "/Home/_Pagination",
            //    "/Home/NotPermission",
            //    "/Home/_NotPermission",
            //    "/Admin/CreateLisence"
            //};
            //string actionName = filterContext.ActionDescriptor.ActionName;
            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionUrl = "/" + controllerName + "/" + actionName;

            //if (notCheck.Any(x => string.Equals(x, actionUrl, System.StringComparison.OrdinalIgnoreCase)))
            //{
            //    return;
            //}
            //if (HttpContext.Current.Session[AppSessionKeys.USER_INFO] == null)
            //{
            //    string url = HttpContext.Current.Request.CurrentExecutionFilePath;
            //    if (!string.Equals(actionUrl, "/Home/Login", System.StringComparison.OrdinalIgnoreCase))
            //    {
            //        string redirect = "/Login";
            //        if (!string.IsNullOrEmpty(url) && url != "/")
            //        {
            //            redirect += "?url=" + url;
            //        }
            //        filterContext.Result = new RedirectResult(redirect);
            //        return;
            //    }
            //}
            //else
            //if (!filterContext.IsChildAction && !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    //List<SYS_ACTIONS> actions = (List<SYS_ACTIONS>)HttpContext.Current.Session[AppSessionKeys.LIST_ACTION];
            //    //if (actions.Any(x => string.Equals(x.ACTION_CONTROLPATH, actionUrl, System.StringComparison.OrdinalIgnoreCase)))
            //    //{
            //    //    return;
            //    //}
            //    //else
            //    //{
            //    //    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() || filterContext.IsChildAction)
            //    //    {
            //    //        if (string.Equals(controllerName, "Ajax", System.StringComparison.OrdinalIgnoreCase))
            //    //        {
            //    //            Models.AjaxResultModel Result = new Models.AjaxResultModel()
            //    //            {
            //    //                Code = 2000,
            //    //                Result = "Bạn không có quyền truy cập chức năng này"
            //    //            };
            //    //            filterContext.Result = new JsonResult { Data = Result };
            //    //            return;
            //    //        }
            //    //        else
            //    //        {
            //    //            ContentResult cr = new ContentResult();
            //    //            cr.Content = ViewRenderer.RazorViewToString<Controllers.HomeController>("_NotPermission", null, true);
            //    //            filterContext.Result = cr;// ViewRenderer.CreateController<Controllers.HomeController>()._NotPermission();
            //    //        }
            //    //    }
            //    //    else
            //    //    {
            //    //        filterContext.Result = new RedirectResult("/Home/NotPermission");
            //    //    }
            //    //}
            //}
        }
    }

    public static class ViewRenderer
    {
        //ViewRenderer.RazorViewToString<Controllers.HomeController>("~/Views/Home/_NotPermission.cshtml", null, true);
        //ActionResult result = ViewRenderer.CreateController<Controllers.AdminController>()._Check();
        public static string RazorViewToString<T>(string viewPath, object model, bool partial = false, System.Web.Routing.RouteData routeData = null) where T : Controllers.BaseController, new()
        {
            #region CreateController

            //Create a disconnected controller instance
            T controller = new T();

            //Get context wrapper from HttpContext if available
            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
            {
                wrapper = new HttpContextWrapper(HttpContext.Current);
            }
            else
            {
                return "Can't create Controller Context if no active HttpContext instance is available.";
            }

            if (routeData == null)
            {
                routeData = new System.Web.Routing.RouteData();
            }

            //Add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
            {
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));
            }

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);

            #endregion

            ControllerContext controllerContext = controller.ControllerContext;

            ViewEngineResult viewEngineResult = null;
            if (partial)
            {
                viewEngineResult = ViewEngines.Engines.FindPartialView(controllerContext, viewPath);
            }
            else
            {
                viewEngineResult = ViewEngines.Engines.FindView(controllerContext, viewPath, null);
            }

            if (viewEngineResult == null || viewEngineResult.View == null)
            {
                return "View could not be found";
            }

            //Get the view and attach the model to view data
            var view = viewEngineResult.View;
            controllerContext.Controller.ViewData.Model = model;

            string result = "";

            using (var writer = new System.IO.StringWriter())
            {
                var viewContext = new ViewContext(controllerContext, view, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, writer);
                view.Render(viewContext, writer);
                result = writer.ToString();
            }

            return result;
        }

        public static T CreateController<T>(System.Web.Routing.RouteData routeData = null) where T : Controllers.BaseController, new()
        {
            //Create a disconnected controller instance
            T controller = new T();

            //Get context wrapper from HttpContext if available
            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
            {
                wrapper = new HttpContextWrapper(HttpContext.Current);
            }
            else
            {
                return null; //"Can't create Controller Context if no active HttpContext instance is available.";
            }

            if (routeData == null)
            {
                routeData = new System.Web.Routing.RouteData();
            }

            //Add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
            {
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));
            }

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);

            return controller;
        }

        public static object CallByName(string namespaceName, string className, string methodName, params object[] obj)
        {
            try
            {
                System.Type t = System.Type.GetType(namespaceName + "." + className);

                foreach (System.Reflection.MethodInfo m in t.GetMethods().Where(x => x.Name == methodName).OrderByDescending(x => x.GetParameters().Count()))
                {
                    System.Reflection.ParameterInfo[] para = m.GetParameters();
                    if (para.Length >= obj.Length)
                    {
                        bool isMatch = true;
                        for (int i = 0; i < obj.Length; i++)
                        {
                            if (obj[i].GetType() != para[i].ParameterType)
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        if (isMatch && (para.Length == obj.Length || para.Length > obj.Length && para[obj.Length].HasDefaultValue))
                        {
                            return m.Invoke(t, obj);
                        }
                    }
                }

                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}