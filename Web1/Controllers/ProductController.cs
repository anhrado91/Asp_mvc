
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    public class ProductController : Controller
    {
        EShopV20 dbc = new EShopV20();

        public ActionResult ListByCategory(int Id)
        {
            Session["ShoppingUrl"] = "/Product/ListByCategory/" + Id;
            var model = dbc.Products
                 .Where(p => p.CategoryId == Id)
                 .ToList();
            return View("ProductList", model);
        }

        public ActionResult ListBySupplier(String Id)
        {
            Session["ShoppingUrl"] = "/Product/ListBySupplier/" + Id;
            var model = dbc.Products
                .Where(p => p.SupplierId == Id)
                .ToList();
            return View("ProductList", model);
        }

        public ActionResult ListBySpecail(String Id)
        {
            Session["ShoppingUrl"] = "/Product/ListBySpecail/" + Id;
            List<Product> model = null;
            switch (Id)
            {
                case "BEST":
                    model = dbc.Products
                        .OrderByDescending(p => p.OrderDetails.Sum(d => d.Quantity))
                        .Take(12)
                        .ToList();
                    break;
                case "LATEST":
                    model = dbc.Products
                        .Where(p => p.Latest == true)
                        .ToList();
                    break;
                case "VIEW":
                    model = dbc.Products
                        .OrderByDescending(p => p.Views)
                        .Take(12)
                        .ToList();
                    break;
                case "SPECAIL":
                    model = dbc.Products
                        .Where(p => p.Special == true)
                        .ToList();
                    break;
                case "DISCOUNT":
                    model = dbc.Products
                        .Where(p => p.Discount > 0)
                        .OrderByDescending(p => p.Discount)
                        .Take(12)
                        .ToList();
                    break;
            }
            return View("ProductList", model);
        }

        public ActionResult Search(String Keywords)
        {
            Session["ShoppingUrl"] = "/Product/Search?Keywords=" + Keywords;
            var model = dbc.Products
                .Where(p => p.Name.Contains(Keywords)
                || p.Category.Name.Contains(Keywords)
                || p.Category.NameVN.Contains(Keywords))
                .ToList();
            return View("ProductList", model);
        }
        public ActionResult Detail(int Id)
        {
            var model = dbc.Products.Find(Id);

            // Tăng số lần xem
            model.Views++;
            dbc.SaveChanges();

            //Ghi nhận hàng đã xem
            var cookie = Request.Cookies["Views"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Views");
            }
            cookie.Values[Id.ToString()] = Id.ToString();
            Response.Cookies.Add(cookie);

            //Truy vấn hàng đã xem
            var Ids = cookie.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            ViewBag.ViewDetails = dbc.Products.Where(p => Ids.Contains(p.Id));

            return View("ProductDetail", model);
        }
    }
}