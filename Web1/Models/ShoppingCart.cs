using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web1.Models
{
    public class ShoppingCart
    {       
        //Danh sách mặt hàng đã chọn
        public List<Product> Items = new List<Product>();
        //Thêm sản phẩm vào giỏ hàng
        public void Add(int Id)
        {
            try
            {
                var Item = Items.Single(p => p.Id == Id);
                Item.Quantity++;
            }
            catch
            {

                using (var dbc = new EShopV20())
                {
                    var Item = dbc.Products.Find(Id);
                    Item.Quantity = 1;
                    Items.Add(Item);
                }
            }
        }


        public double Amount
        {
            get
            {
                var amount = Items.Sum(p => p.UnitPrice * p.Quantity * (1 - p.Discount));
                return amount;
            }
        }

        public int Count
        {
            get
            {
                var count = Items.Sum(p => p.Quantity);
                return count;
            }
        }


        //--Truy xuất giỏ hàng trong session--///
        public static ShoppingCart Cart
        {

            get
            {
                var cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                if (cart == null)
                {
                    cart = new ShoppingCart();
                    HttpContext.Current.Session["ShoppingCart"] = cart;

                }
              
                return cart;
            }
        }

        public void Remove(int Id)
        {
            var Item = Items.Single(p => p.Id == Id);
            Items.Remove(Item);
        }

        public void Update(int Id,int newQuantity)
        {
            var Item = Items.Single(p => p.Id == Id);
            Item.Quantity = newQuantity;
        }

      public double getItemAmount(int Id)
        {
            var Item = Items.Single(p => p.Id == Id);
            return Item.UnitPrice * Item.Quantity * (1 - Item.Discount);
        }
        
        public void Clear()
        {
            Items.Clear();
        }
    }
}