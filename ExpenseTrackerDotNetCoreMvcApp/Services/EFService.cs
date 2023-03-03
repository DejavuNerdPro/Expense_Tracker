
using ExpenseTRacker.Models;
using ExpenseTrackerDotNetCoreMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerDotNetCoreMvcApp.Services
{
    public class EFService : DbContext
    {
        public EFService(DbContextOptions<EFService> options) : base(options)
        { 
        
        }

        public DbSet<ExpenseTrackerDataModel> expense_tracker { get; set; }
    }
}
