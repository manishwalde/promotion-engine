using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain;

namespace webapi.Services
{
   public interface IPromotionService
   {
      Task<IEnumerable<Promotion>> GetAllAsync();
      Task<Promotion> GetPromotionByIdAsync(int Id);
      Task<bool> CreatePromotionAsync(Promotion promotion);
      void UpdatePromotionAsync(Promotion promotion);
      Task<bool> DeletePromotionAsync(Promotion promotion);
      Task<bool> SaveChangesAsync();
   }
}