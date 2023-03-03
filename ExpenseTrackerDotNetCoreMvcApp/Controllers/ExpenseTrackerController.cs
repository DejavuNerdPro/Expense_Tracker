using ExpenseTRacker.Models;
using ExpenseTRacker.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTRacker.Controllers
{
    public class ExpenseTrackerController : Controller
    {
        private readonly DBService DbService;
        public ExpenseTrackerController(DBService db)
        {
            DbService = db;
        }
        public IActionResult ExpenseTrackerIndex()
        {
            return View("ExpenseTrackerIndex");
        }

        [HttpPost]
        public IActionResult ExpenseTrackerAdd(AddTransactionModel transaction)
        {
            string amount = transaction.Amount;
            string valid_amount;
            string sign = amount.Substring(0,1);
            if (sign != "+" && sign != "-")
            {
                sign = "";
                valid_amount = amount;
            }
            else
            {
                if (sign != "-")
                    valid_amount = amount;
                else
                    valid_amount = amount.Substring(1);
                
            }
            string type_flag;
            //var type_flag=(sign!='-')? "income":"expense";
            if (sign != "-" || sign==null)
                type_flag = "income";
            else
                type_flag = "expense";
            ExpenseTrackerDataModel dataModel = new ExpenseTrackerDataModel
            {
               // Id = Convert.ToInt32(Guid.NewGuid().ToString()),
                Date = DateTime.Now,
                Description = transaction.Text,
                TransactionType = transaction.Type,
                Amount = Convert.ToDecimal(valid_amount)
            };
            DbService.getRow.Add(dataModel);
            int result = DbService.SaveChanges();
            return Json(new {isSuccess=true});
        }

        [HttpGet]
        public IActionResult ExpenseTrackerProduce()
        {
            var _list = DbService.getRow.ToList().OrderByDescending(s => s.Date);
                Console.WriteLine(_list);
            
            return Json(new { list=_list});
        }

        [HttpPost]
        public IActionResult ExpenseTrackerRemove(RemoveTransactionModel transaction)
        {
            ExpenseTrackerDataModel model = DbService.getRow.FirstOrDefault(item => item.Id == transaction.TransId);
            DbService.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            DbService.Remove(model);
            int result=DbService.SaveChanges();
            return Json(new {removeTransaction = "Successfully removed." });
        }

        public IActionResult ExpenseTrackerUpdateValues()
        {
            var list = DbService.getRow.ToList();
            int total = list.Sum(i=>Convert.ToInt32(i.Amount));
            var _income = list.Where(i => i.TransactionType == "income").Sum(i => i.Amount);
            var _expense = list.Where(i => i.TransactionType == "expense").Sum(i => i.Amount);
            var _balance = Convert.ToInt32(_income) - Convert.ToInt32(_expense);
            return Json(new { income = _income, expense = _expense, balance=_balance });
        }
    }
}
