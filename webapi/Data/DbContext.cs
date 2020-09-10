using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using webapi.Domain;

namespace webapi.Data
{
   public class DataContext : DbContext
   {
      public DataContext(DbContextOptions<DataContext> options)
          : base(options)
      {
      }

      public DbSet<Sku> Skus { get; set; }
      public DbSet<Promotion> Promotions { get; set; }
   }
}
