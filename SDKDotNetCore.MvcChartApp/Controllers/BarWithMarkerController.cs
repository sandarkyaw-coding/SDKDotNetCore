using Microsoft.AspNetCore.Mvc;
using SDKDotNetCore.MvcChartApp.Models;

namespace DotNetTrainingBatch4.MvcChartApp.Controllers
{
    public class BarWithMarkerController : Controller
    {
        public IActionResult BarWithMarker()
        {
            BarWithMarkerModel model = new BarWithMarkerModel
            {
                Name = "Actual",
                Expect = new List<ExpecxtList>
                {
                    new ExpecxtList
                    {
                        X = "2011",
                        Y = 12,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 14,
                                StrokeWidth = 2,
                                StrokeDashArray = "2",
                                StrokeColor = "#775DD0"
                            }
                        }
                    },
                    new ExpecxtList
                    {
                        X = "2012",
                        Y = 44,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 54,
                                StrokeWidth = 5,
                                StrokeHeight = 10,
                                StrokeColor = "#775DD0"
                            }
                        }
                    },
                    new ExpecxtList
                    {
                        X = "2013",
                        Y = 23,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 13,
                                StrokeWidth = 5,
                                StrokeHeight = 10,
                                StrokeColor = "#775DD0"
                            }
                        }
                    },
                    new ExpecxtList
                    {
                        X = "2014",
                        Y = 34,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 54,
                                StrokeWidth = 5,
                                StrokeHeight = 0,
                                StrokeLineCap = "round",
                                StrokeColor = "#775DD0"
                            }
                        }
                    },
                    new ExpecxtList
                    {
                        X = "2015",
                        Y = 81,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 66,
                                StrokeWidth = 5,
                                StrokeHeight = 10,
                                StrokeColor = "#775DD0"
                            }
                        }
                    },
                    new ExpecxtList
                    {
                        X = "2016",
                        Y = 67,
                        Goals = new List<GoalsList>
                        {
                            new GoalsList
                            {
                                Name = "Expected",
                                Value = 70,
                                StrokeWidth = 5,
                                StrokeHeight = 10,
                                StrokeColor = "#775DD0"
                            }
                        }
                    }
                }
            };
            return View(model);
        }
    }
}
