using API.DTOs;
using API.DTOs.AssetDtos;
using API.DTOs.PODtos;
using API.DTOs.PRDtos;
using API.DTOs.QuotDtos;
using API.DTOs.StockDtos;
using API.DTOs.TaxDtos;
using API.DTOs.UserDtos;
using API.Entities;
using API.Entities.AssetAggregate;
using API.Entities.PRAggregate;
using AutoMapper;
using static API.Controllers.AssetDetailsController;
using static API.DTOs.TaxDtos.CreateTaxDto;

namespace API.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //PurchaseRequisition
            CreateMap<PRDto, PurchaseRequisition>();
            CreateMap<UpdatePRDto, PurchaseRequisition>();
            CreateMap<PurchaseRequisition, GetPRDto>();

            //Quotation
            CreateMap<QuotDto, Quotation>();
            CreateMap<UpdateQuotationDto, Quotation>();

            //PurchasOrder
            CreateMap<PODto, PurchaseOrder>();
            CreateMap<UpdatePODto, PurchaseOrder>();

            //TaxInvoice
            CreateMap<CreateTaxDto, TaxInvoice>()
                .ForMember(dest => dest.TaxItems, opt => opt.MapFrom(src => src.TaxItems));

            CreateMap<TaxItemDto, TaxItem>();
            CreateMap<UpdateTaxDto, TaxInvoice>()
                .ForMember(dest => dest.TaxItems, opt => opt.Ignore()); // Ignore mapping TaxItems

            CreateMap<UpdateTaxDto, TaxItem>();
            CreateMap<AddTaxPicDto, TaxInvoice>();

            //Owner
            CreateMap<Owner, OwnerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OwnerDesc, opt => opt.MapFrom(src => src.OwnerDesc));
            CreateMap<OwnerDto, Owner>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            //Stock
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();

            //Asset
            CreateMap<CreateAssetDto, Asset>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new Owner { Id = src.Owner.Id }))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new Stock { Id = src.Stock.Id }));

            CreateMap<Asset, GetAssetDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => new OwnerDto { Id = src.Owner.Id, OwnerDesc = src.Owner.OwnerDesc }))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => new StockDto { Id = src.Stock.Id, Type = src.Stock.Type }));

            CreateMap<UpdateAssetDto, Asset>()
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.StockId, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.MapFrom((src, dest) => src.Owner != null ? new Owner { Id = src.Owner.Id } : dest.Owner))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom((src, dest) => src.Stock != null ? new Stock { Id = src.Stock.Id } : dest.Stock));

            //AssetDetails
            CreateMap<CreateAssetDetailsDto, AssetDetails>();
            CreateMap<CreateAssetDetailsRequest, AssetDetails>();

            CreateMap<AddAssetPicDto, AssetDetails>();
            CreateMap<AssetDetails, GetAssetDetailsDto>();
            CreateMap<AssetDetails, GetAssetDetailsRequest>()
                .ForMember(dest => dest.AssetDetailsDto, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.PersonInChargeDto, opt => opt.MapFrom(src => src.PersonInCharge));
            
            CreateMap<UpdateAssetDetailsDto, AssetDetails>();

            //Person In Charge
            CreateMap<PICDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, GetPICDto>();

        }
    }
}