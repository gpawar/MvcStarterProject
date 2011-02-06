using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcStarterProject.Business;

namespace MvcStarterProject.DataAccess
{
    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }
    }

    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Product> Products { get; set; }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}