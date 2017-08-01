using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web1.Utils
{
    public class AuthenticateAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = HttpContext.Current.Session["User"];
            if(user==null)
            {
                //Duy trì địa chỉ trang cần truy cập vào Session
                var RequestUri = HttpContext.Current.Request.Url.AbsoluteUri;
                HttpContext.Current.Session["RequestUri"] = RequestUri;

                // chuyển về trang đăng nhập
                HttpContext.Current.Response.Redirect("/Action/Login");
            }
        }
    }
}