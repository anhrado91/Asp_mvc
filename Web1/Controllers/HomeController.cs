using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web1.Models;

namespace Web1.Controllers
{
    public class HomeController : Controller
    {
        EShopV20 dbc = new EShopV20();
        public ActionResult Index()
        {
            Session["ShoppingUrl"] = "/Home/Index";
            #region Dictionary
            //Dictionary<int, List<Product>> sanpham = new Dictionary<int, List<Product>>();
            //var sup = dbc.Suppliers.OrderBy(c => c.Id).Take(4).ToList();
            //var product = dbc.Products.ToList();
            //var idex = 0;
            //foreach (var item in sup)
            //{
            //    var fourSP = product.Where(c => c.SupplierId == item.Id).OrderByDescending(c => c.Id).Take(4).ToList();
            //    sanpham.Add(idex++, fourSP);
            //}
            //ViewBag.Suppliers = sanpham;
            #endregion
            ViewBag.Suppliers = dbc.Suppliers
                .Where(s => s.Products.Count >= 4)
                .OrderBy(s => Guid.NewGuid())
                .Take(5)
                .ToList();

            ViewBag.Specials = dbc.Products
                 .Where(p => p.Special)
                 .OrderBy(s => Guid.NewGuid())
                 .ToList();

            ViewBag.Categories = dbc.Categories
                .Where(s => s.Products.Count >= 5)
                .OrderBy(s => Guid.NewGuid())
                .Take(3)
                .ToList();
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
        public ActionResult _Category()
        {
            var model = dbc.Categories.ToList();

            return PartialView(model);
        }
        public ActionResult _Supplier()
        {
            var model = dbc.Suppliers.ToList();
            return PartialView(model);
        }
    }
}