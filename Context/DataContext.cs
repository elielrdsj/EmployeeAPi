using Microsoft.EntityFrameworkCore;
using NovoProjeto.Models;



namespace EmployeeManagementAPI.Context
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> EmployeeDb { get; set; }
    }
}
