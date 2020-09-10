using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webapi.Contracts.Requests;
using webapi.Contracts.Responses;
using webapi.Domain;
using webapi.Services;

namespace AConnect.API.Controllers
{
   [ApiController]
   [Route("api/v1/CalculatePromotion")]
   public class CalculatePromotionController : ControllerBase
   {
      private readonly IPromotionService _PromotionService;
      private readonly ISkuService _skuService;
      private readonly IMapper _mapper;
      public CalculatePromotionController(IPromotionService PromotionService, ISkuService skuService, IMapper mapper)
      {
         _PromotionService = PromotionService;
         _skuService = skuService;
         _mapper = mapper;
      }

      /// <summary>
      /// Calculate Promotion in the system
      /// </summary>
      /// <response code="201">Calculate Promotion in the system</response>
      /// <response code="400">Unable to create Promotion due to validation error</response>
      [HttpPost]
      public async Task<IActionResult> CalculatePromotion([FromBody] CalculatePromotionRequest request)
      {
         var AllPromotion = await _PromotionService.GetAllAsync();
         var AllSkus = await _skuService.GetAllAsync();
         var LowestNoOfUnit = -999;
         var IgnoreSku = new List<string>();

         CalculatePromotionResponse response = new CalculatePromotionResponse();
         response.CartItem = new List<webapi.Contracts.Responses.CartItem>();

         foreach (var cartItem in request.CartItem)
         {
            var matchedPromotion = AllPromotion.Where(x => x.SkuIds.Contains(cartItem.SkuId));

            var matchedSku = AllSkus.FirstOrDefault(x => x.SkuId == cartItem.SkuId);

            if (matchedPromotion == null)
            {
               response.Total += (matchedSku == null) ? 0 : matchedSku.Price;
               response.CartItem.Add(new webapi.Contracts.Responses.CartItem
               {
                  SkuId = cartItem.SkuId,
                  NumberOfUnit = cartItem.NumberOfUnit
               });
            }
            else
            {
               var firstMatchedPromotion = matchedPromotion.FirstOrDefault();
               var skuIds = firstMatchedPromotion.SkuIds.Split(',').ToList();

               if (skuIds.Count == 1)
               {
                  var promotionAppliedGroupTotal = (cartItem.NumberOfUnit / firstMatchedPromotion.NumberOfUnit) * firstMatchedPromotion.ForPrice;
                  var promotionNonAppliedGroupTotal = (cartItem.NumberOfUnit % firstMatchedPromotion.NumberOfUnit) * matchedSku.Price;

                  response.Total += (promotionAppliedGroupTotal + promotionNonAppliedGroupTotal);
                  response.CartItem.Add(new webapi.Contracts.Responses.CartItem
                  {
                     SkuId = cartItem.SkuId,
                     NumberOfUnit = cartItem.NumberOfUnit
                  });
               }
               else
               {
                  var allSkuIdsInCart = true;
                  foreach (var skuid in skuIds)
                  {
                     var found = request.CartItem.FirstOrDefault(x => x.SkuId == skuid);

                     if (found == null)
                     {
                        allSkuIdsInCart = false;
                        break;
                     }

                     if (LowestNoOfUnit == -999)
                     {
                        LowestNoOfUnit = found.NumberOfUnit;
                     }
                     else if (found.NumberOfUnit < LowestNoOfUnit)
                     {
                        LowestNoOfUnit = found.NumberOfUnit;
                     }
                  }
                  if (!allSkuIdsInCart)
                  {
                     response.Total += (cartItem.NumberOfUnit * matchedSku.Price);
                     response.CartItem.Add(new webapi.Contracts.Responses.CartItem
                     {
                        SkuId = cartItem.SkuId,
                        NumberOfUnit = cartItem.NumberOfUnit
                     });
                  }
                  else
                  {
                     if (!IgnoreSku.Contains(cartItem.SkuId))
                     {
                        response.Total += (cartItem.NumberOfUnit * firstMatchedPromotion.ForPrice);
                     }
                     response.CartItem.Add(new webapi.Contracts.Responses.CartItem
                     {
                        SkuId = cartItem.SkuId,
                        NumberOfUnit = cartItem.NumberOfUnit
                     });
                     
                     skuIds.Remove(cartItem.SkuId);
                     IgnoreSku = skuIds;

                  }
               }
            }
         }

         foreach (var promotion in AllPromotion)
         {
            var skuIds = promotion.SkuIds.Split(',');

            if (skuIds.Length == 1)
            {

            }
         }

         return Ok(response);

      }
   }
}