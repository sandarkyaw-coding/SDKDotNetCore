using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        private readonly ILogger<CanvasJsController> _logger;

        public CanvasJsController(ILogger<CanvasJsController> logger)
        {
            _logger = logger;
        }

        public IActionResult LineChart()
        {
            _logger.LogInformation("Line Chart..");
            return View();
        }

        public IActionResult AxisAreaChart() {
            return View();
        }

        public IActionResult LiveColumnChart()
        {
            return View();
        }
    }
}
