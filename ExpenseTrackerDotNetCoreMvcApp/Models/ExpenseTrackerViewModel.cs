using System;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class ExpenseTrackerViewModel
    {
        public int id { get; set; }
        public string description { get; set; }
        public string transaction_type { get; set; }
        public decimal amount { get; set; }
        public DateTime date { get; set; }
    }
}
