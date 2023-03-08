using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    [Table("TblLogin")]
    public class LoginDataModel
    {
        [Key]
        public int login_id { get; set; }
        public string user_unique_id { get; set; }
        public string session_id { get; set; }
        public DateTime session_exp_datetime { get; set; }
        public DateTime logout_datetime { get; set; }
        public string reason { get; set; }

    }
}

