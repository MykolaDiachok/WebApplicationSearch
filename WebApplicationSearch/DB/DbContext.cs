using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationSearch.Models
{
    public class DBContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
            
        }
        public DbSet<Result> Results { get; set; }
    }
}
