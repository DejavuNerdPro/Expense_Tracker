using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class CustomMonthModel
    {
        public int amount { get; set; }
        public string transaction_type { get; set; }
        public string show_date { get; set; }
    }

    public class CustomMonthPieChartModel
    {
        public List<int> CustomMonthPieChartLabel { get; set; }
    }
}
