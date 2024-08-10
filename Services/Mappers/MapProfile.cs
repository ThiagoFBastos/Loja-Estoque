using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Services.Mappers
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Store, StoreDto>();

            CreateMap<StoreForCreateDto, Store>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.StockItems, opt => opt.Ignore());

            CreateMap<StoreForUpdateDto, Store>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.StockItems, opt => opt.Ignore());

            CreateMap<Product, ProductDto>();

            CreateMap<ProductForCreateDto, Product>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.StockItems, opt => opt.Ignore());

            CreateMap<ProductForUpdateDto, Product>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.StockItems, opt => opt.Ignore());

            CreateMap<StockItem, StockItemDto>();

            CreateMap<StockItemForCreateDto, StockItem>()
                .ForMember(e => e.Product, opt => opt.Ignore())
                .ForMember(e => e.Store, opt => opt.Ignore());

            CreateMap<StockItemForUpdateDto, StockItem>()
                .ForMember(e => e.Product, opt => opt.Ignore())
                .ForMember(e => e.Store, opt => opt.Ignore())
                .ForMember(e => e.ProductId, opt => opt.Ignore())
                .ForMember(e => e.StoreId, opt => opt.Ignore());
        }
    }
}
