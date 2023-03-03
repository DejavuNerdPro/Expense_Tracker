namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class ExpenseTrackerRequsetModel
    {
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
