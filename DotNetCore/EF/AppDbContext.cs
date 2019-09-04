using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.EF
{
    public class AppDbContext : DbContext //lt + Enter for using
    {
        //ctor for constructor
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
