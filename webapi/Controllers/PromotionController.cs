using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webapi.Contracts.Requests;
using webapi.Contracts.Responses;
using webapi.Domain;
using webapi.Services;

namespace AConnect.API.Controllers.V1
{
   [ApiController]
   [Route("api/v1/Promotion")]
   public class PromotionController : ControllerBase
   {
      private readonly IPromotionService _PromotionService;
      private readonly IMapper _mapper;
      public PromotionController(IPromotionService PromotionService, IMapper mapper)
      {
         _PromotionService = PromotionService;
         _mapper = mapper;
      }

      /// <summary>
      /// Returns Promotion in the system
      /// </summary>
      /// <response code="200">Returns Promotion in the system</response>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         var Promotion = await _PromotionService.GetAllAsync();
         return Ok(_mapper.Map<IEnumerable<PromotionResponse>>(Promotion));
      }

      /// <summary>
      /// Returns Promotion by id in the system
      /// </summary>
      /// <response code="200">Returns Promotion by id in the system</response>
      /// <response code="400">Request not found</response>
      [HttpGet("{Id}", Name = "GetPromotionById")]
      public async Task<IActionResult> GetPromotionById([FromRoute] int Id)
      {
         var Promotion = await _PromotionService.GetPromotionByIdAsync(Id);

         if (Promotion != null)
         {
            return Ok(_mapper.Map<PromotionResponse>(Promotion));
         }
         return NotFound();
      }

      /// <summary>
      /// Create Promotion in the system
      /// </summary>
      /// <response code="201">Create Promotion in the system</response>
      /// <response code="400">Unable to create Promotion due to validation error</response>
      [HttpPost]
      public async Task<IActionResult> CreateAddress([FromBody] PromotionRequest request)
      {
         var Promotion = _mapper.Map<Promotion>(request);

         var created = await _PromotionService.CreatePromotionAsync(Promotion);

         if (!created)
            return BadRequest(new { error = "Unable to create Promotion" });

         return CreatedAtRoute(nameof(GetPromotionById), new { Id = Promotion.Id }, _mapper.Map<PromotionResponse>(Promotion));
      }

      /// <summary>
      /// Update Promotion in the system
      /// </summary>
      /// <response code="201">Create a Promotion in the system</response>
      /// <response code="400">Unable to create Promotion due to validation error</response>
      [HttpPut("{Id}")]
      public async Task<IActionResult> UpdateAddress(int Id, PromotionRequest request)
      {
         var Promotion = await _PromotionService.GetPromotionByIdAsync(Id);

         if (Promotion == null)
         {
            return NotFound();
         }

         _mapper.Map(request, Promotion);

         _PromotionService.UpdatePromotionAsync(Promotion);

         await _PromotionService.SaveChangesAsync();

         return Ok(_mapper.Map<PromotionResponse>(Promotion));
      }

      /// <summary>
      /// Delete Promotion in the system
      /// </summary>
      /// <response code="201">Delete Promotion in the system</response>
      /// <response code="400">Unable to delete Promotion due to validation error</response>
      [HttpDelete("{Id}")]
      public async Task<IActionResult> DeleteAddress([FromRoute] int Id)
      {
         var Promotion = await _PromotionService.GetPromotionByIdAsync(Id);

         if (Promotion == null)
         {
            return NotFound();
         }

         var deleted = await _PromotionService.DeletePromotionAsync(Promotion);

         if (deleted)
            return NoContent();

         return NotFound();
      }
   }
}