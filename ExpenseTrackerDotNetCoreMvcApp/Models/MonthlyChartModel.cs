using System;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class MonthlyChartModel
    {
        public int amount { get; set; }
        public string transaction_type { get; set; }
        public int show_date { get; set; }
    }
}
