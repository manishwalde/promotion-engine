using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Contracts.Requests;
using webapi.Contracts.Responses;
using webapi.Domain;
using webapi.Services;

namespace AConnect.API.Controllers.V1
{
   [ApiController]
   [Route("api/v1/sku")]
   public class SkuController : ControllerBase
   {
      private readonly ISkuService _skuService;
      private readonly IMapper _mapper;
      public SkuController(ISkuService skuService, IMapper mapper)
      {
         _skuService = skuService;
         _mapper = mapper;
      }

      /// <summary>
      /// Returns skus in the system
      /// </summary>
      /// <response code="200">Returns skus in the system</response>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         var sku = await _skuService.GetAllAsync();
         return Ok(_mapper.Map<IEnumerable<SkuResponse>>(sku));
      }

      /// <summary>
      /// Returns sku by id in the system
      /// </summary>
      /// <response code="200">Returns sku by id in the system</response>
      /// <response code="400">Request not found</response>
      [HttpGet("{Id}", Name = "GetSkuById")]
      public async Task<IActionResult> GetSkuById([FromRoute] int Id)
      {
         var sku = await _skuService.GetSkuByIdAsync(Id);

         if (sku != null)
         {
            return Ok(_mapper.Map<SkuResponse>(sku));
         }
         return NotFound();
      }

      /// <summary>
      /// Create sku in the system
      /// </summary>
      /// <response code="201">Create sku in the system</response>
      /// <response code="400">Unable to create sku due to validation error</response>
      [HttpPost]
      public async Task<IActionResult> CreateAddress([FromBody] SkuRequest request)
      {
         var sku = _mapper.Map<Sku>(request);

         var created = await _skuService.CreateSkuAsync(sku);

         if (!created)
            return BadRequest(new { error = "Unable to create sku" });

         return CreatedAtRoute(nameof(GetSkuById), new { Id = sku.Id }, _mapper.Map<SkuResponse>(sku));
      }

      /// <summary>
      /// Update sku in the system
      /// </summary>
      /// <response code="201">Create a sku in the system</response>
      /// <response code="400">Unable to create sku due to validation error</response>
      [HttpPut("{Id}")]
      public async Task<IActionResult> UpdateAddress(int Id, SkuRequest request)
      {
         var sku = await _skuService.GetSkuByIdAsync(Id);

         if (sku == null)
         {
            return NotFound();
         }

         _mapper.Map(request, sku);

         _skuService.UpdateSkuAsync(sku);

         await _skuService.SaveChangesAsync();

         return Ok(_mapper.Map<SkuResponse>(sku));
      }

      /// <summary>
      /// Delete sku in the system
      /// </summary>
      /// <response code="201">Delete sku in the system</response>
      /// <response code="400">Unable to delete sku due to validation error</response>
      [HttpDelete("{Id}")]
      public async Task<IActionResult> DeleteAddress([FromRoute] int Id)
      {
         var sku = await _skuService.GetSkuByIdAsync(Id);

         if (sku == null)
         {
            return NotFound();
         }

         var deleted = await _skuService.DeleteSkuAsync(sku);

         if (deleted)
            return NoContent();

         return NotFound();
      }
   }
}