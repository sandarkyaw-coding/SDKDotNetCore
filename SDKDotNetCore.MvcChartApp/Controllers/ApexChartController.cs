using Microsoft.AspNetCore.Mvc;
using SDKDotNetCore.MvcChartApp.Models;

namespace SDKDotNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            PieChartModel model =  new PieChartModel();
            model.Labels = new List<string>() { "Team A", "Team B", "Team C", "Team D", "Team E" };
            model.Series = new List<int> { 44, 55, 13, 43, 22 };
            return View(model);
        }
    }
}
