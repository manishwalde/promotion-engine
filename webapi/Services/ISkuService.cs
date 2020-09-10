using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Sku;

namespace webapi.Services
{
   public interface ISkuService
   {
      Task<IEnumerable<Sku>> GetAllAsync();
      Task<bool> CreateSkuAsync(Sku sku);
      void UpdateSkuAsync(Sku sku);
      Task<bool> DeleteSkuAsync(Sku sku);
      Task<bool> SaveChangesAsync();
   }
}