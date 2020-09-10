using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapi.Domain;

namespace webapi.Contracts.Responses
{
   public class CalculatePromotionResponse
   {
      public List<CartItem> CartItem { get; set; }
      public float Total { get; set; }
   }
   public class CartItem
   {
      public int NumberOfUnit { get; set; }
      public string SkuId { get; set; }
   }

}