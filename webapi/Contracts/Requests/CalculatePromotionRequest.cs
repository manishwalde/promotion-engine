using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapi.Domain;

namespace webapi.Contracts.Requests
{
   public class CalculatePromotionRequest
   {
      public List<CartItem> CartItem { get; set; }
   }

   public class CartItem
   {
      [Required]
      public int NumberOfUnit { get; set; }
      [Required]
      public string SkuId { get; set; }

   }
}