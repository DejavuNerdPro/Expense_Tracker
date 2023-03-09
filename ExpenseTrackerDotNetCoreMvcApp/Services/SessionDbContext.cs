using ExpenseTrackerDotNetCoreMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerDotNetCoreMvcApp.Services {
    public class SessionDbContext : DbContext {
        public SessionDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<LoginDataModel> session { get; set; }
    }
}
