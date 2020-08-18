using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationSearch.Models;

namespace WebApplicationSearch.DB
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            if (!context.Database.CanConnect())
                context.Database.EnsureCreated();
        }
    }
}
