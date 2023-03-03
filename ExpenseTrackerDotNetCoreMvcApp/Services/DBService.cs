using ExpenseTRacker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTRacker.Services
{
    public class DBService :DbContext
    {
        public DBService(DbContextOptions<DBService> options) : base(options)
        {

        }
        public DbSet<ExpenseTrackerDataModel> getRow { get; set; }
    }
}
