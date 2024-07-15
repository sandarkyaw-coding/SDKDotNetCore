using Microsoft.AspNetCore.Mvc;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }

        public IActionResult StellarChart()
        {
            return View();
        }
        public IActionResult ThreeD_BubblesChart()
        {
            return View();
        }
    }
}
