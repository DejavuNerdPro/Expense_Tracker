using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Models
{
    [Table("TblUser")]
    public class UserDataModel
    {
		[Key]
        public int user_id { get; set; }
		public string user_unique_id { get; set; }
		public string user_name { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string role { get; set; }
		public DateTime create_date_time { get; set; }
		public string create_user_id { get; set; }
		public DateTime modified_date_time { get; set; }
		public string modified_user_id { get; set; }
		public int del_flag { get; set; }

	}
}
