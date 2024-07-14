using Microsoft.AspNetCore.Mvc;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult LineChart()
        {
            return View();
        }
    }
}
