namespace webapi.Domain
{
   public class Promotion
   {
      public int Id { get; set; }
      public int NumberOfUnit { get; set; }
      public string SkuIds { get; set; }
      public float ForPrice { get; set; }
      public PromotionType PromotionType { get; set; }
   }
}