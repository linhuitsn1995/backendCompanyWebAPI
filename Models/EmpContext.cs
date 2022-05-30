using Microsoft.EntityFrameworkCore;

namespace ASPWebAPIFirstCode.Models
{
    public class EmpContext: DbContext
    {
        public EmpContext(DbContextOptions options): base(options) { }
        public DbSet <Employees> Employees { get; set; }
    }
}
