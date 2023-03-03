using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using System.Linq;
using ExpenseTrackerDotNetCoreMvcApp.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Resources;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class AddTransactionController : Controller
    {
        private readonly EFService _db;
        private readonly DapperService _dapperService;

        public AddTransactionController(EFService db, DapperService dapperService)
        {
            _db = db;
            _dapperService = dapperService;
        }

        public IActionResult AddTransactionIndex()
        {
            return View();
        }

        public IActionResult AddTransactionDetail(int pageNo = 1, int rowCount = 5)
        {
            //var list = _db.expense_tracker.OrderByDescending(x => x.Id).ToList();

            string query = $@"select * from Tbl_ExpenseTracker 
where MONTH(Date) = MONTH(GetDate()) and YEAR(Date) = YEAR(GetDate())
order by date desc
OFFSET {(pageNo - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY";

            string queryCount = @"select count(id) from Tbl_ExpenseTracker 
where MONTH(Date) = MONTH(GetDate()) and YEAR(Date) = YEAR(GetDate())";

            var list = _dapperService.GetList<ExpenseTrackerDataModel>(query);
            var count = _dapperService.GetItem<int>(queryCount);

            var lst = list.Select(x => new ExpenseTrackerViewModel
            {
                id = x.Id,
                amount = x.Amount,
                date = x.Date,
                description = x.Description,
                transaction_type = x.TransactionType,
            }).ToList();


            int totalPageNo = count / rowCount;
            int result = count % rowCount;
            if (result > 0)
                totalPageNo++;

            ExpenseTrackerResponseViewModel model = new ExpenseTrackerResponseViewModel();
            model.lstExpenseTracker = lst;
            model.TotalRowCount = count;
            model.RowCount = rowCount;
            model.TotalPageNo = totalPageNo;
            model.CurrentPageNo = pageNo;

            return View("AddTransactionDetail", model);
        }

        [HttpPost]
        public IActionResult AddTransactionAdd(ExpenseTrackerRequsetModel reqModel)
        {
            if (reqModel.TransactionType == "Income")
            {

                ExpenseTrackerDataModel model = new ExpenseTrackerDataModel
                {
                    Description = reqModel.Description,
                    Amount = reqModel.Amount,
                    Date = DateTime.Now,
                    TransactionType = reqModel.TransactionType,
                };
                _db.expense_tracker.Add(model);
                _db.SaveChanges();
            }
            else
            {

                ExpenseTrackerDataModel model = new ExpenseTrackerDataModel
                {
                    Description = reqModel.Description,
                    Amount = reqModel.Amount,
                    Date = DateTime.Now,
                    TransactionType = reqModel.TransactionType,
                };
                _db.expense_tracker.Add(model);
                _db.SaveChanges();
            }
            decimal IncomeAmount = _db.expense_tracker.Where(x => x.TransactionType == "Income").Sum(i => i.Amount);
            decimal ExpenseAmount = _db.expense_tracker.Where(x => x.TransactionType == "Expense").Sum(i => i.Amount);
            decimal Balance = IncomeAmount - ExpenseAmount;
            return Json(new
            {
                IncomeAmount = IncomeAmount,
                ExpenseAmount = ExpenseAmount,
                Balance = Balance
            });
        }

        [HttpPost]
        public IActionResult Delete(DeleteModel reqModel)
        {
            var item = _db.expense_tracker.FirstOrDefault(x => x.Id == reqModel.DeleteId);
            _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _db.expense_tracker.Remove(item);
            _db.SaveChanges();
            decimal IncomeAmount = _db.expense_tracker.Where(x => x.TransactionType == "Income").Sum(i => i.Amount);
            decimal ExpenseAmount = _db.expense_tracker.Where(x => x.TransactionType == "Expense").Sum(i => i.Amount);
            decimal Balance = IncomeAmount - ExpenseAmount;
            return Json(new
            {
                IncomeAmount = IncomeAmount,
                ExpenseAmount = ExpenseAmount,
                Balance = Balance
            });
        }

        public IActionResult AddTransactionAddDays()
        {
            int total = 0;
            DateTime currentDate = DateTime.Now;
            DateTime addYear;
            List<int> years = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                addYear = currentDate.AddYears(-1 * i);
                years.Add(addYear.Year);
            }

            List<string> TransactionType = new List<string>();
            TransactionType.Add("Income");
            TransactionType.Add("Expense");
            List<string> description_income = new List<string>()
            {
                "Salary","Bonus","OT","Other",
            };
            List<string> description_expense = new List<string>()
            {
                "Rent","Telephone","Gas","Electric",
            };
            Random random = new Random();
            //for (int i = 0; i < 3; i++)
            //{
            //    DateTime start_date = new DateTime(years[i], 1, 1);
            //    DateTime end_date = new DateTime(years[i], 12, 31);
            //    int range = (end_date - start_date).Days;
            //    int randomDays = random.Next(range);
            //    DateTime randomDate = start_date.AddDays(randomDays);
            //}
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 365; j++)
                {
                    total = i;
                    string transaction_type = TransactionType[random.Next(0, 2)];
                    decimal Amount = random.Next(100000, 1000000);

                    DateTime start_date = new DateTime(years[i], 1, 1);
                    DateTime end_date = new DateTime(years[i], 12, 31);
                    int range = (end_date - start_date).Days;
                    int randomDays = random.Next(range);
                    DateTime randomDate = start_date.AddDays(randomDays);

                    if (transaction_type == "Income")
                    {
                        string Description = description_income[random.Next(0, 4)];
                        string query = $@"INSERT INTO [dbo].[Tbl_ExpenseTracker]
                                   ([Date]
                                   ,[Description]
                                   ,[TransactionType]
                                   ,[Amount])
                                    VALUES
                                   ('{randomDate}'
                                   ,'{Description}'
                                   ,'{transaction_type}'
                                   ,'{Amount}')";
                        int result = _dapperService.Execute(query);
                        //ExpenseTrackerDataModel model = new ExpenseTrackerDataModel
                        //{
                        //    TransactionType = transaction_type,
                        //    Date = currentDate,
                        //    Description = description_income[random.Next(0, 4)],
                        //    Amount = random.Next(100000, 1000000)
                        //};
                        //_db.expense_tracker.Add(model);
                    }
                    else
                    {
                        string Description = description_expense[random.Next(0, 4)];
                        string query = $@"INSERT INTO [dbo].[Tbl_ExpenseTracker]
                                   ([Date]
                                   ,[Description]
                                   ,[TransactionType]
                                   ,[Amount])
                                    VALUES
                                   ('{randomDate}'
                                   ,'{Description}'
                                   ,'{transaction_type}'
                                   ,'{Amount}')";
                        int result = _dapperService.Execute(query);
                        //ExpenseTrackerDataModel model = new ExpenseTrackerDataModel
                        //{
                        //    TransactionType = transaction_type,
                        //    Date = currentDate,
                        //    Description = description_expense[random.Next(0, 4)],
                        //    Amount = random.Next(10000, 100000)
                        //};
                        //_db.expense_tracker.Add(model);
                    }
                }
            }
            //int result = _db.SaveChanges();
            return Json(new { isSuccess = total });
        }
    }
}
