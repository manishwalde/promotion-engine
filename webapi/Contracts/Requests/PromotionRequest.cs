using System.ComponentModel.DataAnnotations;
using webapi.Domain;

namespace webapi.Contracts.Requests
{
   public class PromotionRequest
   {
      [Required]
      public int NumberOfUnit { get; set; }
      [Required]
      public string SkuIds { get; set; }
      [Required]
      public float ForPrice { get; set; }

      [Required]
      [EnumDataType(typeof(PromotionType))]
      public string PromotionType { get; set; }
   }
}