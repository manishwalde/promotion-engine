using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain;

namespace webapi.Services
{
   public interface ISkuService
   {
      Task<IEnumerable<Sku>> GetAllAsync();
      Task<Sku> GetSkuByIdAsync(int Id);
      Task<bool> CreateSkuAsync(Sku sku);
      void UpdateSkuAsync(Sku sku);
      Task<bool> DeleteSkuAsync(Sku sku);
      Task<bool> SaveChangesAsync();
   }
}