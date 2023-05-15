using API.DTOs.TaxDtos;
using API.DTOs.PODtos;
using API.DTOs.PRDtos;
using API.DTOs.QuotDtos;
using API.Entities;
using API.Entities.PRAggregate;
using AutoMapper;
using static API.DTOs.TaxDtos.CreateTaxDto;

namespace API.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PRDto, PurchaseRequisition>();
            CreateMap<UpdatePRDto, PurchaseRequisition>();
            CreateMap<PurchaseRequisition, GetPRDto>();

            CreateMap<QuotDto, Quotation>();
            CreateMap<UpdateQuotationDto, Quotation>();

            CreateMap<PODto, PurchaseOrder>();

            // Inside your AutoMapper configuration
            CreateMap<CreateTaxDto, TaxInvoice>()
                .ForMember(dest => dest.TaxItems, opt => opt.MapFrom(src => src.TaxItems));

            // Ensure that you have a mapping for TaxItemDto as well
            CreateMap<TaxItemDto, TaxItem>();

            CreateMap<UpdateTaxDto, TaxInvoice>();
        }
    }
}