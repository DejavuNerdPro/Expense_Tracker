using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class BaseController : Controller
    {
        public void SetSession(string key, object value)
        {
            HttpContext.Session.SetString(key,value.ToJson());
        }

        public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        public T GetSession<T>(string key)
        {
            return HttpContext.Session.GetString(key).ToObject<T>();
        }

        public List<T> GetSessionList<T>(string key)
        {
            return HttpContext.Session.GetString(key).ToObjectList<T>();
        }
    }
}
