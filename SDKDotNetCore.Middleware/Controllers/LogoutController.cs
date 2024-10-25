using Microsoft.AspNetCore.Mvc;

namespace SDKDotNetCore.Middleware.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
