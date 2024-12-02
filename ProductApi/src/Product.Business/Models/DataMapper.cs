using AutoMapper;

namespace Product.Business.Models;

public class DataMapper : Profile
{
    public DataMapper()
    {
        CreateMap<ProductPayload, DataAccess.Entities.Product>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
            .ForPath(dest => dest.Category.CategoryName, opt => opt.MapFrom(src => src.Category))
            .ReverseMap();

        CreateMap<ProductCategoryPayload, DataAccess.Entities.ProductCategory>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}
