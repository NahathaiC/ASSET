using API.DTOs.AssetDtos;
using API.DTOs.PODtos;
using API.DTOs.PRDtos;
using API.DTOs.QuotDtos;
using API.DTOs.StockDtos;
using API.DTOs.TaxDtos;
using API.Entities;
using API.Entities.AssetAggregate;
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

            CreateMap<TaxItemDto, TaxItem>();
            CreateMap<UpdateTaxDto, TaxInvoice>();
            CreateMap<AddTaxPicDto, TaxInvoice>();

            CreateMap<CreateAssetDto, Asset>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new Owner { Id = src.Owner.Id }))
                .ForMember(dest => dest.Stock, opt => opt.Ignore()); // Ignore mapping StockDto to Stock

            CreateMap<CreateAssetDetailsDto, AssetDetails>();

            CreateMap<CreateStockDto, Stock>();
        }
    }
}
