using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FacebookId"],
                client_secret = ConfigurationManager.AppSettings["FacebookSC"],
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        private string fullNameFb = "";
        private string facebookId = "";
        private string emailFb = "";
        public ActionResult Login()
        {
            ViewBag.Email = emailFb;
            ViewBag.FullName = fullNameFb;
            ViewBag.FaceBookId = facebookId;
            return View();
        }
        public ActionResult FacebookCallback(string code)
        {

            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FacebookId"],
                client_secret = ConfigurationManager.AppSettings["FacebookSC"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var access_token = result.access_token;
            if (!string.IsNullOrEmpty(access_token))
            {
                fb.AccessToken = access_token;
                //neu co thong tin tu token
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string fullName = me.first_name + " " + me.middle_name + " " + me.last_name;
                string fbId = me.id;
                facebookId = fbId;
                emailFb = email;
                fullNameFb = fullName;
            }
            return Redirect("/Home/Login");
        }
    }
}