using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class YearlyReportController : Controller
    {
        private readonly DapperService _db;

        public YearlyReportController(DapperService db)
        {
            _db = db;
        }

        public IActionResult YearlyReportIndex()
        {
            ViewBag.line_chart = YearlyReportLineChart();
            return View("YearlyReportIndex");
        }

        public LineChartModel YearlyReportLineChart()
        {
            string query = @"select SUM(Amount) amount, YEAR(Date) show_date,
                            TransactionType transaction_type
                            from [dbo].[Tbl_ExpenseTracker]
                            group by YEAR(Date),TransactionType
                            order by YEAR(Date)";
            var list = _db.GetList<MonthlyChartModel>(query);
            //string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            //List<string> lst = monthNames.ToList();
            //lst.RemoveAt(12);
            //monthNames = lst.ToArray();
            List<int> filter = new List<int>();
            List<int> count = new List<int>();
            count = list.GroupBy(x => x).Select(i => i.First().show_date).ToList();
            for (int i = 0; i < count.Count; i++)
            {
                if (i % 2 != 0)
                {
                    filter.Add(count[i]);
                }
            }
            LineChartModel model = new LineChartModel
            {
                LineChartIncomeLabel = list.Where(x => x.transaction_type == "Income")
                                       .Select(x => x.amount).ToList(),
                LineChartExpenseLabel = list.Where(x => x.transaction_type == "Expense")
                                       .Select(x => x.amount).ToList(),
                LineCharYearLabel = filter,
            };
            return model;
        }
    }
}
