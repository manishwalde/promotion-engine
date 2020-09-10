using AutoMapper;
using webapi.Contracts.Responses;
using webapi.Domain;

namespace webapi.Profiles
{
   public class DomainToResponseProfiles : Profile
   {
      public DomainToResponseProfiles()
      {
         CreateMap<Sku, SkuResponse>();
      }
   }
}