using System;
using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class LineChartModel
    {
        public List<int> LineCharYearLabel { get; set; }
        public List<int> LineChartExpenseLabel { get; set; }
        public List<int> LineChartIncomeLabel { get; set; }
    }
}
