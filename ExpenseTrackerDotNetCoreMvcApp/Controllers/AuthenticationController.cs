using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DapperService dapperService;
        public AuthenticationController(DapperService _dapperService)
        {
            dapperService = _dapperService;
        }
        public IActionResult AuthenticationIndex()
        {
            return View("AuthenticationIndex");
        }

        public IActionResult AuthenticationLogin(Login login)
        {
            string email = login.Email;
            string password = login.Password;
            string query = $@"SELECT * FROM [dbo].[TblUser] WHERE email={email} AND password={email}";
            var model = dapperService.GetItem<UserDataModel>(query);
            if (model == null)
                return Json(new { isSuccess = false });
            else
                return Json(new { isSuccess = true });
        }

       
        public IActionResult AuthenticationRegistration()
        {
            return View("AuthenticationRegistration");
        }

        [HttpPost]
        public IActionResult AuthenticationRegistrationValidation(RegistrationFormModel registrationData)
        {
            //var user_uniqueId = Guid.NewGuid().ToString();
            var user_uniqueId = "user_unique_key1";
            var user_name = registrationData.user_name;
            var password = registrationData.password;
            var email = registrationData.email;
            var role = "member";
            var del_flag = 0;
            var session_id = "unset_yet";
            var now = DateTime.Now;
            
            string insert_user_table_query = $@"INSERT INTO[dbo].[TblUser]
            ([user_unique_id],[user_name],[password],[email],[role],[del_flag])
            VALUES
           ({user_uniqueId},{user_name},{password},{email},{role},{del_flag})";

            string insert_login_table_query = $@"INSERT INTO [dbo].[TblLogin]
           ([user_unique_id],[session_id],[session_exp_datetime],[logout_datetime])
            VALUES
          ({user_uniqueId},{session_id},{now},{now})";

            string all_dataRow_query = $@"SELECT * FROM [dbo].[TblUser]";
            int user_flag, login_flag;
            //check if user already exist or not
            var login_data_list = dapperService.GetList<UserDataModel>(all_dataRow_query).ToList();
            var user = login_data_list.Select(i=>i.user_unique_id==user_uniqueId).ToList();
            if (user == null)
            {
                return Json(new { message="user_already_exist"});
            }
            else
            {
                user_flag = dapperService.Execute(insert_user_table_query);
                login_flag = dapperService.Execute(insert_login_table_query);
            }

            if(user_flag==1 && login_flag==1)
            {
                return Json(new { message = "transaction_done" });
            }

            return null;
            
        }

    }
}
