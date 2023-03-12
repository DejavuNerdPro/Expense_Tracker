using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Middleware {
    public class SessionMiddleware {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next) {
            _next = next;
        }

        string signInUrl = "/Authentication";
        string logOutUrl = "/Authentication/Logout";
       
        //initial possible routes
        List<string> passUrl = new List<string>
        {
            "/Authentication",
            "/Authentication/Index",
            "/Authentication/AuthenticationLogin",
            "/Authentication/AuthenticationRegistration",
            "/Authentication/AuthenticationRegistrationValidation",
            "/ExpenseTrackerReport/ExpenseTrackerReportIndex",
            "/ExpenseTracker/ExpenseTrackerIndex",
            "/TransactionHistory/TransactionHistoryIndex",
            "/YearlyReport/YearlyReportIndex",
            "/DayByDayReport/DayByDayReportIndex",
            "/CurrentMonthReport/CurrentMonthReportIndex"
        };

        public async Task InvokeAsync(HttpContext context, SessionDbContext _db) {
            string url = context.Request.Path;

            if (passUrl.Count(x => x.ToLower() == url.ToLower()) > 0) goto result;

            if (url.ToLower() == signInUrl.ToLower()) goto result;

            if (context.Session.GetString("UserId") == null ||
                context.Session.GetString("SessionId") == null)
            {
                context.Response.Redirect(signInUrl);
            }

            string userId = context.Session.GetString("UserId");
            string sessionId = context.Session.GetString("SessionId");

            if (userId == null || sessionId == null ||
                string.IsNullOrEmpty(userId.Trim()) || string.IsNullOrEmpty(sessionId.Trim()))
            {
                context.Response.Redirect(signInUrl);
            }
            var item = await _db.session
                .FirstOrDefaultAsync(x =>
                    x.user_unique_id == userId &&
                    x.session_id == sessionId);
            

            if (item == null)
            {
                context.Response.Redirect(signInUrl);
            }

            if (item != null && item.session_exp_datetime <= DateTime.Now)
            {
                context.Session.Clear();
                context.Response.Redirect(signInUrl);
            }
        result:
            await _next(context);
        }
    }

    public static class SessionMiddlewareExtension {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder app) {
            return app.UseMiddleware<SessionMiddleware>();
        }
    }
}
