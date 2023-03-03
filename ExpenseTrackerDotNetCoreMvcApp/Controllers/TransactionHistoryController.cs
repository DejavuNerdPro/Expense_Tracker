using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class TransactionHistoryController : Controller
    {
        private readonly EFService _db;

        public TransactionHistoryController(EFService db)
        {
            _db = db;
        }

        public IActionResult TransactionHistoryIndex()
        {
            
            return View("TransactionHistoryIndex");
        }

        public IActionResult TransactionHistoryDetail()
        {
            var lst = _db.expense_tracker.ToList();
            var list = lst.Select(x => new ExpenseTrackerViewModel
            {
                id = x.Id,
                date = x.Date,
                amount = x.Amount,
                description = x.Description,
                transaction_type = x.TransactionType,
            }).ToList();
            return View("TransactionHistoryDetail",list);
        }
    }
}
