using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web1.Models;
using Web1.Utils;

namespace Web1.Controllers
{
    public class AccountController : Controller
    {
        
        EShopV20 dbc = new EShopV20();
        public ActionResult Login()
        {
          
            var cookie = Request.Cookies["user"];
            if (cookie != null) // đặt dữ liệu trong cookie lên form
            {
                ViewBag.Id = cookie.Values["Id"];
                ViewBag.Password = cookie.Values["Pw"].Decode();
                ViewBag.Remember = true;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(String Id, String Password, Boolean Remember)
        {
            var user = dbc.Customers.Find(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Sai tên đăng nhập");
            }
            else if (user.Password != Password)
            {
                ModelState.AddModelError("", "Sai mật khẩu");
            }
            else if (!user.Activated)
            {
                ModelState.AddModelError("", "tài khoản chưa kích hoạt");
            }
            else
            {
                ModelState.AddModelError("", "đăng nhập thành công");

                Session["User"]= user;

                var cookie = new HttpCookie("user");
                if (Remember)
                {
                    cookie.Values["Id"] = Id;
                    cookie.Values["Pw"] = Password.Encode();
                    cookie.Expires = DateTime.Now.AddMonths(1);
                }
                else
                {
                    cookie.Expires = DateTime.Now;
                }
                Response.Cookies.Add(cookie);


                // Trở lại trang đã yêu cầu trước đó
                var RequestUri = Session["RequestUri"] as String;
                if (RequestUri != null)
                {
                    return Redirect(RequestUri);
                }
            }
            return View();
        }

        //Duy trì địa chỉ trang cần truy cập vào session
        [Authenticate]
        public ActionResult Logoff()
        {
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(Customer model)
        {
            try
            {
                var file=Request.Files["upPhoto"]
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}