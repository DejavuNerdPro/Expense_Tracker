using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class ExpenseTrackerResponseViewModel
    {
        public int TotalRowCount { get; set; }
        public int TotalPageNo { get; set; }
        public int RowCount { get; set; }
        public int CurrentPageNo { get; set; }
        public List<ExpenseTrackerViewModel> lstExpenseTracker { get; set; }
    }
}
