using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    public class HeaderModel
    {
        public HeaderModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
