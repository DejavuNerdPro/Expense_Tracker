using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExpenseTrackerDotNetCoreMvcApp.Services
{
    public static class DevCode
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static List<T> ToObjectList<T>(this string str)
        {
            return JsonConvert.DeserializeObject<List<T>>(str);
        }
    }
}
