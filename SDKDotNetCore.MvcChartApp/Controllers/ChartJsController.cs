using Microsoft.AspNetCore.Mvc;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ExampleChart()
        {
            return View();
        }

        public IActionResult InterpolationModes()
        {
            return View();
        }

        public IActionResult LegendPointStyle()
        {
            return View();
        }

        public IActionResult FloatingBar()
        {
            return View();
        }
    }
}
