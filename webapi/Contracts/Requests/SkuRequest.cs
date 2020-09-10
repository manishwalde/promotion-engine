using System.ComponentModel.DataAnnotations;

namespace webapi.Contracts.Requests
{
   public class SkuRequest
   {
      [Required]
      [MaxLength(1)]
      public string SkuId { get; set; }
      [Required]
      public float Price { get; set; }
   }
}