using AutoMapper;
using webapi.Contracts.Requests;
using webapi.Domain;

namespace webapi.Profiles
{
   public class RequestToDomainProfiles : Profile
   {
      public RequestToDomainProfiles()
      {
         CreateMap<SkuRequest, Sku>();
      }
   }
}