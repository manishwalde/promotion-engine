using System.ComponentModel.DataAnnotations;
using webapi.Domain;

namespace webapi.Contracts.Responses
{
   public class PromotionResponse
   {
      public int Id { get; set; }
      public int NumberOfUnit { get; set; }
      public string SkuIds { get; set; }
      public float ForPrice { get; set; }
      public string PromotionType { get; set; }
   }
}