using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class DayByDayReportController : BaseController
    {
        private readonly DapperService _db;

        public DayByDayReportController(DapperService db)
        {
            _db = db;
        }

        public IActionResult DayByDayReportIndex()
        {
            return View();
        }

        public IActionResult DayByDayReportCustomMonth(ExpenseTrackerCustomMonthModel reqModel)
        {
            string query = $@"select SUM(Amount) amount,CONVERT(date,Date) show_date,
                            TransactionType transaction_type
                            from [dbo].[Tbl_ExpenseTracker] 
                            where CONVERT(date, Date) = '{reqModel.report_month}'
                            group by CONVERT(date,Date), TransactionType
                            ";
            var list = _db.GetList<CustomMonthModel>(query);
            CustomMonthPieChartModel model = new CustomMonthPieChartModel
            {
                CustomMonthPieChartLabel = list.Select(x=> x.amount).ToList(),
            };

            HttpContext.Session.SetString("CustomMonthModel",
                model.ToJson());
            //SetSession("CustomMonthModel", model);

            return Json(new
            {
                isSuccess = true
            });
        }

        public IActionResult DayByDayReportDetail()
        {
            var list = JsonConvert.DeserializeObject<CustomMonthPieChartModel>
                        (HttpContext.Session.GetString("CustomMonthModel"));

            return View("DayByDayReportDetail", list);
        }
    }
}
