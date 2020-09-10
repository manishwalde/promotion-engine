using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Domain.Sku;

namespace webapi.Services
{
   public class SkuService : ISkuService
   {
      private readonly DataContext _dataContext;
      public SkuService(DataContext dataContext)
      {
         _dataContext = dataContext;
      }

      public async Task<bool> CreateSkuAsync(Sku sku)
      {
         if (sku == null)
         {
            throw new ArgumentNullException(nameof(sku));
         }
         await _dataContext.Skus.AddAsync(sku);
         return await SaveChangesAsync();
      }

      public async Task<bool> DeleteSkuAsync(Sku sku)
      {
         if (sku == null)
         {
            throw new ArgumentNullException(nameof(sku));
         }
         _dataContext.Skus.Remove(sku);
         return await SaveChangesAsync();
      }

      public async Task<IEnumerable<Sku>> GetAllAsync()
      {
         return await _dataContext.Skus.ToListAsync();
      }

      public async Task<bool> SaveChangesAsync()
      {
         return (await _dataContext.SaveChangesAsync() > 0);
      }

      public void UpdateSkuAsync(Sku sku)
      {
         throw new NotImplementedException();
      }
   }
}