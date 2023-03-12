using ExpenseTRacker.Services;
using ExpenseTrackerDotNetCoreMvcApp.Models;
using ExpenseTrackerDotNetCoreMvcApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerDotNetCoreMvcApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DapperService dapperService;
        private readonly DBService _db;
        public AuthenticationController(DapperService _dapperService, DBService db)
        {
            dapperService = _dapperService;
            _db = db;
        }
        
        [ActionName("Index")]
        public IActionResult AuthenticationIndex()
        {
            return View("AuthenticationIndex");
        }

//        login 
//-username and password are same? welcome back
//-store user_unique Id and session Id in session
//-if both id in session is not null welcome back
//-implement hash code in password


        public IActionResult AuthenticationLogin(Login login)
        {
            string email = login.Email;
            string password =login.Password;
            string _session_id = Guid.NewGuid().ToString();
            DateTime _session_exp_datetime = DateTime.Now.AddDays(1);
            DateTime _logout_datetime = new DateTime(2023, 02, 09, 11, 01, 45);
            string _reason = "login";
            var userModel = _db.getUserData.FirstOrDefault(i => i.email == email);
            bool isVerifiedPassword = BCrypt.Net.BCrypt.Verify(password,userModel.password);
            if(userModel!=null  && isVerifiedPassword)
            {
                var data_in_login = _db.getLoginData.FirstOrDefault(i => i.user_unique_id == userModel.user_unique_id);
                if (data_in_login==null)
                {
                    LoginDataModel loginModel = new LoginDataModel()
                    {
                        user_unique_id = userModel.user_unique_id,
                        session_id = _session_id,
                        session_exp_datetime = _session_exp_datetime,
                        logout_datetime = _logout_datetime,
                        reason = _reason
                    };
                    _db.getLoginData.Add(loginModel);
                    _db.SaveChanges();

                }
                    HttpContext.Session.SetString("UserId", userModel.user_unique_id);
                    HttpContext.Session.SetString("SessionId", _session_id);
                    
                    return Json(new { isSuccess = true });
            }
            else
            {
                return Json(new { isSuccess = false });
            }
            
        }

        
        public IActionResult AuthenticationRegistration()
        {
            return View("AuthenticationRegistration");
        }

        [HttpPost]
        public IActionResult AuthenticationRegistrationValidation(RegistrationFormModel registrationData)
        {
            var user_uniqueId = Guid.NewGuid().ToString();
            var user_name = registrationData.user_name;
            var password = BCrypt.Net.BCrypt.HashPassword(registrationData.password);
            var email = registrationData.email;
            var role = "member";
            var del_flag = 0;
            DateTime create_date_time = DateTime.Now;
            string create_user_id = "user";
            DateTime modified_date_time = DateTime.Now;
            string modified_user_id = "user";
           
            string insert_user_table_query = $@"INSERT INTO [dbo].[TblUser]
           ([user_unique_id]
           ,[user_name]
           ,[password]
           ,[email]
           ,[role]
           ,[create_date_time]
           ,[create_user_id]
           ,[modified_date_time]
           ,[modified_user_id]
           ,[del_flag])
     VALUES
           ('{user_uniqueId}'
           ,'{user_name}'
           ,'{password}'
           ,'{email}'
           ,'{role}'
           ,'{create_date_time}'
           ,'{create_user_id}'
           ,'{modified_date_time}'
           ,'{modified_user_id}'
           ,'{del_flag}')";

            string all_dataRow_query = $@"SELECT * FROM [dbo].[TblUser]";
            int user_flag;
            
            var login_data_list = dapperService.GetList<UserDataModel>(all_dataRow_query).ToList();
            var user = login_data_list.FirstOrDefault(i=>i.user_name ==user_name);
            if (user != null)
            {
                return Json(new { message="user_already_exist"});
            }
            else
            {
                user_flag = dapperService.Execute(insert_user_table_query);
               
                if (user_flag == 1)
                {
                    return Json(new { message = "transaction_done" });
                }
            }
            return null;

        }
        
        public IActionResult Logout()
        {
            string user_uniqueId = HttpContext.Session.GetString("UserId");
           
            DateTime _logout_datetime = DateTime.Now;
            DateTime _session_exp_datetime = DateTime.Now;
            string _reason = "logout";

            var model =_db.getLoginData.FirstOrDefault(i => i.user_unique_id == user_uniqueId);
            LoginDataModel loginModel = new LoginDataModel()
            {
                user_unique_id = user_uniqueId,
                session_id = model.session_id,
                session_exp_datetime = _session_exp_datetime,
                logout_datetime = _logout_datetime,
                reason = _reason
            };

            _db.Entry(loginModel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_db.getLoginData.Update(loginModel);
           // _db.SaveChanges();
            HttpContext.Session.Clear();
            return View("AuthenticationIndex");
        }

    }
}
