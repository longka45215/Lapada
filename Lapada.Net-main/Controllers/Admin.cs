using Microsoft.AspNetCore.Mvc;

namespace WEB_Shop_PRN.Controllers
{
    public class Admin : Controller
    {
        public IActionResult CheckOrder()
        {
            return View();
        }
    }
}
