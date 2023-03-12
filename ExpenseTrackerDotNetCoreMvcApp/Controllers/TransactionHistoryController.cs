using ExpenseTRacker.Models;
using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class TransactionHistoryController : Controller
    {
        private readonly DapperService _db;

        public TransactionHistoryController(DapperService db)
        {
            _db = db;
        }

        public IActionResult TransactionHistoryIndex()
        {
            
            return View("TransactionHistoryIndex");
        }
        public IActionResult TransactionHistoryDetailYear(int pageIndex)
        {
            int index = pageIndex;
            int pageNo=index==0? 1:index;
            int rowCount = 5;
            string queryCount = @"select count(id) from [dbo].[Tbl_ExpenseTracker]
                                where Date 
                                BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                                ";
            var count = _db.GetItem<int>(queryCount);
            pageNo = index == count ? count : index;

            string query = $@"select * from [dbo].[Tbl_ExpenseTracker]
                            where Date
                            BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                            order by date desc
                            OFFSET {(pageNo - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY
                            ";
            var list = _db.GetList<ExpenseTrackerDataModel>(query);
           
            
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

            return View("TransactionHistoryDetailYear", model);
        }
       
        public IActionResult TransactionHistoryDetailMonth(int pageIndex)
        {
            int index = pageIndex;
            int pageNo=index==0? 1:index;
            int rowCount = 5;
            string queryCount = @"select count(id) from [dbo].[Tbl_ExpenseTracker]
                                where Date 
                                BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                                ";
            var count = _db.GetItem<int>(queryCount);
            pageNo = index == count ? count : index;

            string query = $@"select * from [dbo].[Tbl_ExpenseTracker]
                            where Date
                            BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                            order by date desc
                            OFFSET {(pageNo - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY
                            ";
            var list = _db.GetList<ExpenseTrackerDataModel>(query);

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

            return View("TransactionHistoryDetailMonth", model);
        }
       
        public IActionResult TransactionHistoryDetail90Days(int pageIndex)
        {
            int index = pageIndex;
            int pageNo = index == 0 ? 1 : index;
            int rowCount = 5;
            string queryCount = @"select count(id) from [dbo].[Tbl_ExpenseTracker]
                                where Date 
                                BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                                ";
            var count = _db.GetItem<int>(queryCount);
            pageNo = index == count ? count : index;

            string query = $@"select * from [dbo].[Tbl_ExpenseTracker]
                            where Date
                            BETWEEN  DATEFROMPARTS(YEAR(GETDATE()), 1, 1) AND GETDATE()
                            order by date desc
                            OFFSET {(pageNo - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY
                            ";
            var list = _db.GetList<ExpenseTrackerDataModel>(query);

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

            return View("TransactionHistoryDetail90Days", model);
        }
    }
}
