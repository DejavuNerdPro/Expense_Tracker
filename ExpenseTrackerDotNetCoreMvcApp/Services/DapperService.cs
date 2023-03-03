using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExpenseTrackerDotNetCoreMvcApp.Services
{
    public class DapperService
    {
        private readonly string _connection;
        public DapperService(IConfiguration configuration) 
        {
            _connection = configuration.GetConnectionString("DbConnection");
        }

        public List<T> GetList<T>(string query)
        {
            using(IDbConnection db = new SqlConnection(_connection))
            {
                var list = db.Query<T>(query).ToList();
                return list;
            }
        }

        public T GetItem<T>(string query)
        {
            using(IDbConnection db = new SqlConnection(_connection))
            {
                var item = db.Query<T>(query).FirstOrDefault();
                return item;
            }
        }

        public int Execute(string query)
        {
            using(IDbConnection db = new SqlConnection(_connection))
            {
                int result = db.Execute(query);
                return result;
            }
        }
    }
}
