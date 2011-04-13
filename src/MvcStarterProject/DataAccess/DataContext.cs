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
        DbSet<Order> Orders { get; set; }
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    public class DataContext : DbContext, IDataContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(m => m.Products)
                .WithMany()
                .Map(m => m.MapLeftKey(o => o.OrderId, "OrderId")
                              .MapRightKey(p => p.ProductId, "ProductId"));
        }
    }
}