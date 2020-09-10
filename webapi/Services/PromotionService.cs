using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Domain;

namespace webapi.Services
{
   public class PromotionService : IPromotionService
   {
      private readonly DataContext _dataContext;
      public PromotionService(DataContext dataContext)
      {
         _dataContext = dataContext;
      }

      public async Task<bool> CreatePromotionAsync(Promotion promotion)
      {
         if (promotion == null)
         {
            throw new ArgumentNullException(nameof(promotion));
         }
         await _dataContext.Promotions.AddAsync(promotion);
         return await SaveChangesAsync();
      }

      public async Task<bool> DeletePromotionAsync(Promotion promotion)
      {
         if (promotion == null)
         {
            throw new ArgumentNullException(nameof(promotion));
         }
         _dataContext.Promotions.Remove(promotion);
         return await SaveChangesAsync();
      }

      public async Task<IEnumerable<Promotion>> GetAllAsync()
      {
         return await _dataContext.Promotions.ToListAsync();
      }

      public async Task<Promotion> GetPromotionByIdAsync(int Id)
      {
         return await _dataContext.Promotions.SingleOrDefaultAsync(x => x.Id == Id);
      }

      public async Task<bool> SaveChangesAsync()
      {
         return (await _dataContext.SaveChangesAsync() > 0);
      }

      public void UpdatePromotionAsync(Promotion promotion)
      {
         // Nothing
      }
   }
}