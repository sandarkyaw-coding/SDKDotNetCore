namespace SDKDotNetCore.MvcChartApp.Models
{
    /*   public class BarWithMarkerModel
       {
           public string Name { get; set; }
           public int Value { get; set; }
           public int StrokeWidth { get; set; }
           public int StrokeHeight { get; set; }
           public string StrokeDashArray { get; set; }
           public string StrokeLineCap { get; set; }
           public string StrokeColor { get; set; }
       }

       public class DataSet {
           public string X { get; set; }
           public int Y { get; set; }
           public List<BarWithMarkerModel> Goals { get; set; }
       }

       public class Series
       {
           public string Name { get; set; }
           public List<DataSet> Data { get; set; }
       }*/

    public class GoalsList
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int StrokeWidth { get; set; }
        public int StrokeHeight { get; set; }
        public string StrokeDashArray { get; set; }
        public string StrokeLineCap { get; set; }
        public string StrokeColor { get; set; }
    }

    public class ExpecxtList
    {
        public string X { get; set; }
        public int Y { get; set; }
        public List<GoalsList> Goals { get; set; }
    }


    public class BarWithMarkerModel
    {
        public string Name { get; set; }
        public List<ExpecxtList> Expect { get; set; }
    }

}




