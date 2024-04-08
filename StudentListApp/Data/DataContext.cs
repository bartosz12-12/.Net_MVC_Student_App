using Microsoft.EntityFrameworkCore;
using StudentListApp.Models.Student;
using StudentListApp.Models.Zajecia;

namespace StudentListApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<StudentModel> Student => Set<StudentModel>();
        public DbSet<ZajeciaModel> Zajecias => Set<ZajeciaModel>();
    }
}
