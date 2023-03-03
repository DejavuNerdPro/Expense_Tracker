using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class CurrentMonthReportController : Controller
    {
        private readonly DapperService _db;

        public CurrentMonthReportController(DapperService db)
        {
            _db = db;
        }

        public IActionResult CurrentMonthReportIndex()
        {
            ViewBag.pie_chart = CurrentMonthReportPieChart();
            return View();
        }

        public PieChartModel CurrentMonthReportPieChart()
        {
            string query = @"select SUM(Amount) amount,CONVERT(varchar(7), Date, 126) show_date,
                                TransactionType transaction_type
                                from [dbo].[Tbl_ExpenseTracker] 
                                where CONVERT(varchar(7), Date, 126) = CONVERT(varchar(7), GETDATE() ,126)
                                group by CONVERT(varchar(7), Date, 126), TransactionType";
            var list = _db.GetList<CurrentMonthModel>(query);
            //List<int> income = new List<int>();
            //List<int> expense = new List<int>();
            //expense = list.Where(x => x.transaction_type == "Expense").Select(x => x.amount).ToList();
            //income = list.Where(x => x.transaction_type == "Income").Select(x => x.amount).ToList();
            //string income_percent = Convert.ToString((Convert.ToDouble(expense[0]) / income[0]) * 100) + "%"; 
            PieChartModel model = new PieChartModel
            {
                //PieChartYearLabel = list.Select(x=> x.show_date).ToList(),
                //PieChartIncomeLabel = list.Where(x => x.transaction_type == "Income")
                //                    .Select(x => x.amount).ToList(),
                //PieChartExpenseLabel = list.Where(x => x.transaction_type == "Expense")
                //                    .Select(x => x.amount).ToList(),
                PieChartLabel = list.Select(x => x.amount).ToList(),
            };
            return model;
        }
    }
}
