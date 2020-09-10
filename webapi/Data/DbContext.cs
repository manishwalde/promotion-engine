using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using webapi.Domain.Sku;

namespace webapi.Data
{
   public class DataContext : DbContext
   {
      public DataContext(DbContextOptions<DataContext> options)
          : base(options)
      {
      }

      public DbSet<Sku> Skus { get; set; }
   }
}
