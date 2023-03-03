using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTRacker.Models
{
    public class AddTransactionModel
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
    }
}
