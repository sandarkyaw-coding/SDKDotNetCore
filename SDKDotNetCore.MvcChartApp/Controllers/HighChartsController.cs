using Microsoft.AspNetCore.Mvc;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
