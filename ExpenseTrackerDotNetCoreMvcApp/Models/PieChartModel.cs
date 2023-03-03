using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class PieChartModel
    {
        public List<string> PieChartYearLabel { get; set; }
        public List<int> PieChartExpenseLabel { get; set; }
        public List<int> PieChartIncomeLabel { get; set; }
        public List<int> PieChartLabel { get; set; }
    }

    public class CurrentMonthModel
    {
        public int amount { get; set; }
        public string transaction_type { get; set; }
        public string show_date { get; set; }
    }
}
