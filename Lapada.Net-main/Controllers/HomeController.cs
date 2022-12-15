using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using WEB_Shop_PRN.Models;

namespace WEB_Shop_PRN.Controllers
{
    public class HomeController : Controller
    {
        WebAppOrderContext context = new WebAppOrderContext();
        static User loginUser = null;
        public IActionResult Home()
        {
            List<Product> products = context.Products.OrderByDescending(item => item.ProductPrice).ToList();
            ViewBag.Products = products;
            return View();
        }
        public IActionResult Detail(int id)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.product = product;
            return View();
        }
        public IActionResult Search(string searchLapada)
        {
            var product = context.Products.Where(p => p.ProductName.Contains(searchLapada)).ToList();
            ViewBag.product = product;
            var size = product.Count;
            ViewBag.key = searchLapada;
            ViewBag.size = size;

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string name, string pass)
        {

            User user = context.Users.FirstOrDefault(u => u.UserName.Equals(name) && u.UserPassword.Equals(pass));
            if (user != null)
            {
                loginUser = user;
                ViewBag.User = loginUser;
                return RedirectToAction("Admin");
            }
            else
            {
                ViewBag.mess = "Invalid username or password";
                return RedirectToAction("Login");
            }

        }
        public IActionResult DetailProduct(int id)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductId == id);
            List<Product> products = context.Products.ToList();
            ViewBag.Products = products;
            ViewBag.product = product;
            return View();
        }

        public IActionResult BuyProduct(int quantity, int idPro)
        {
            using (WebAppOrderContext context = new WebAppOrderContext())
            {
                Cart check = context.Carts.FirstOrDefault(c => c.ProductId == idPro);
                if (check == null)
                {
                    Cart cart = new Cart
                    {
                        ProductAmount = quantity,
                        UserId = "admin",
                        ProductId = idPro
                    };
                    context.Carts.Add(cart);
                }
                else
                {
                    check.ProductAmount += quantity;
                }
                context.SaveChanges();
                return RedirectToAction("Cart");

            }

        }
        public IActionResult BuyNowProduct(int quantity, int idPro)
        {
            using (WebAppOrderContext context = new WebAppOrderContext())
            {
                Cart cart = new Cart
                {
                    ProductAmount = quantity + 1,
                    UserId = "admin",
                    ProductId = idPro
                };
                Product product = context.Products.FirstOrDefault(p => p.ProductId == idPro);
                ViewBag.product = product;
                ViewBag.cart = cart;
                ViewBag.Price = product.ProductPrice * cart.ProductAmount;
                return View();

            }

        }
        public IActionResult Delete(int id)
        {
            Cart cart = context.Carts.FirstOrDefault(p => p.CartId == id);
            context.Carts.Remove(cart);
            context.SaveChanges();
            return RedirectToAction("Cart");

        }
        public IActionResult Cart()
        {
            List<Product> products = context.Products.ToList();
            List<Cart> carts = context.Carts.ToList();
            ViewBag.Products = products;
            ViewBag.Carts = carts;
            return View();
        }
        public IActionResult Confirm(int amount, int price, int idpro, string address, string nameUser)
        {
            using (WebAppOrderContext context = new WebAppOrderContext())
            {
                Address add = new Address
                {
                    AddressDetail = address,
                    CustomerName = nameUser,
                    CityId = "1",
                    DistrictId = "1"
                };
                context.Addresses.Add(add);
                context.SaveChanges();
                var orders = context.Addresses.OrderByDescending(item => item.AddressId).Take(1).ToList();

                Order cart = new Order
                {
                    ProductAmount = amount,
                    OrderTotalPrice = price,
                    OrderStatus = 1,
                    UserId = "admin",
                    AddressId = orders[0].AddressId,
                    ProductId = idpro
                };
                context.Orders.Add(cart);
                context.SaveChanges();
                return View();

            }
        }
        public IActionResult Buy()
        {
            List<Product> products = context.Products.ToList();
            List<Cart> carts = context.Carts.ToList();
            ViewBag.Products = products;
            ViewBag.Carts = carts;
            return View();
        }

        public IActionResult Admin()
        {
            List<Product> products = context.Products.ToList();
            List<Order> order = context.Orders.ToList();
            List<Address> add = context.Addresses.ToList();
            ViewBag.Products = products;
            ViewBag.Address = add;
            ViewBag.Order = order;
            return View();
        }
        public IActionResult SearchOrder(string search)
        {
            List<Product> products = context.Products.ToList();
            List<Order> order = context.Orders.ToList();
            List<Address> add = context.Addresses.Where(p => p.CustomerName.Contains(search)).ToList();
            ViewBag.Products = products;
            ViewBag.Address = add;
            ViewBag.Order = order;
            return View();
        }
        public IActionResult Ship(int id)
        {

            Order oder = context.Orders.FirstOrDefault(p => p.OrderId == id);
            oder.OrderStatus = 2;
            context.SaveChanges();
            return RedirectToAction("Admin");
        }
    }
}
