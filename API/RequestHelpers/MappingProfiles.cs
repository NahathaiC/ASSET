using API.DTOs;
using API.DTOs.AssetDtos;
using API.DTOs.PODtos;
using API.DTOs.PRDtos;
using API.DTOs.QuotDtos;
using API.DTOs.StockDtos;
using API.DTOs.UserDtos;
using API.Entities;
using API.Entities.AssetAggregate;
using API.Entities.PRAggregate;
using AutoMapper;
using static API.Controllers.AssetDetailsController;

namespace API.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // PurchaseRequisition
            CreateMap<PRDto, PurchaseRequisition>();
            CreateMap<UpdatePRDto, PurchaseRequisition>();
            CreateMap<PurchaseRequisition, GetPRDto>();

            // Quotation
            CreateMap<CreateQuotDto, Quotation>();
            CreateMap<UpdateQuotationDto, Quotation>();
            CreateMap<QuotDto, Quotation>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // PurchaseOrder
            CreateMap<PODto, PurchaseOrder>();
            CreateMap<UpdatePODto, PurchaseOrder>();

            // Owner
            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OwnerDesc, opt => opt.MapFrom(src => src.OwnerDesc));
            CreateMap<OwnerDto, Owner>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            // Stock
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();
            CreateMap<Stock, StockDto>();

            // // Asset
            // CreateMap<CreateAssetDto, Asset>()
            //     .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new Owner { Id = src.Owner.Id }))
            //     .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new Stock { Id = src.Stock.Id }))
            //     .ForMember(dest => dest.PersonInCharge, opt => opt.Ignore());

            // CreateMap<Asset, GetAssetDto>()
            //     .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new OwnerDto { Id = src.Owner.Id, OwnerDesc = src.Owner.OwnerDesc }))
            //     .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new StockDto { Id = src.Stock.Id, Type = src.Stock.Type }));

            // CreateMap<UpdateAssetDto, Asset>()
            //     .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            //     .ForMember(dest => dest.StockId, opt => opt.Ignore())
            //     .ForMember(dest => dest.Owner, opt => opt.Ignore())
            //     .ForMember(dest => dest.Stock, opt => opt.Ignore())
            //     .ForMember(dest => dest.PersonInChargeId, opt => opt.Ignore());

            // Asset
            CreateMap<CreateAssetDto, Asset>();
                // .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new Owner { Id = src.Owner.Id }))
                // .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new Stock { Id = src.Stock.Id }))
                // .ForMember(dest => dest.PersonInChargeId, opt => opt.MapFrom(src => src.PersonInCharge.Id));
            CreateMap<CreateAssetDetailsRequest, Asset>()
                .ForMember(dest => dest.PersonInCharge, opt => opt.Ignore());

            CreateMap<Asset, GetAssetDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new OwnerDto { Id = src.Owner.Id, OwnerDesc = src.Owner.OwnerDesc }))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new StockDto { Id = src.Stock.Id, Type = src.Stock.Type }));

            CreateMap<UpdateAssetDto, Asset>()
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.StockId, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Stock, opt => opt.Ignore());

            // AssetDetails
            CreateMap<AssetDetails, AssetDetailsDto>()
                .ForMember(dest => dest.TaxInvoiceId, opt => opt.MapFrom(src => src.TaxInvoice.Id));

            CreateMap<CreateAssetDetailsDto, AssetDetails>();
            CreateMap<CreateAssetDetailsRequest, AssetDetails>()
                .ForMember(dest => dest.PersonInCharge, opt => opt.Ignore());

            CreateMap<AddAssetPicDto, AssetDetails>();
            CreateMap<AssetDetails, GetAssetDetailsDto>();

            CreateMap<AssetDetails, GetAssetDetailsRequest>()
                .ForMember(dest => dest.AssetDetailsDto, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.PersonInChargeDto, opt => opt.MapFrom(src => src.PersonInCharge))
                .ForMember(dest => dest.TaxInvoiceDto, opt => opt.MapFrom(src => src.TaxInvoice));

            // Person In Charge
            CreateMap<PICDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, GetPICDto>();
        }
    }
}
