using ExpenseTRacker.Models;
using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class PaginationController : Controller
    {
        public readonly DapperService dapperService;
        public PaginationController(DapperService _dapperService)
        {
            dapperService = _dapperService;
        }

        [HttpPost]
        public IActionResult PaginationIndexingWithValues(PageIndex transaction)
        {
            int index = transaction.pageIndex;
            int rowCount = 5;
            string query = $@"select * from Tbl_ExpenseTracker 
                             where YEAR(Date) = YEAR(GetDate())
                             order by date desc
                             OFFSET {(index - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY";
            var list = dapperService.GetList<ExpenseTrackerDataModel>(query).ToList();
            return Json(new { data = list });
        }
        [HttpPost]
        public IActionResult PaginationIndexingInitial()
        {
            int index = 1;
            int rowCount = 5;
            string query = $@"select * from Tbl_ExpenseTracker 
                             where MONTH(Date) = MONTH(GetDate()) and YEAR(Date) = YEAR(GetDate())
                             order by date desc
                             OFFSET {(index - 1) * rowCount} ROWS FETCH NEXT {rowCount} ROWS ONLY";
            var list = dapperService.GetList<ExpenseTrackerDataModel>(query).ToList();
            return Json(new { data = list });
        }
    }
}
