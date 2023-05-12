using API.DTOs.PRDtos;
using API.DTOs.QuotDtos;
using API.Entities;
using API.Entities.PRAggregate;
using AutoMapper;

namespace API.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PRDto, PurchaseRequisition>();
            CreateMap<UpdatePRDto, PurchaseRequisition>();
            CreateMap<QuotDto, Quotation>();
            CreateMap<UpdateQuotationDto, Quotation>();
            CreateMap<PurchaseRequisition, GetPRDto>();
        }
    }
}