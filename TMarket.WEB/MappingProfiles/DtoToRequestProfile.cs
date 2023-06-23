using AutoMapper;
using TMarket.Application.ServiceModels;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Commands.CategoryCommands;
using TMarket.WEB.Commands.UserCommands;
using TMarket.WEB.RequestModels;
using TMarket.WEB.RequestModels.Cart;
using TMarket.WEB.RequestModels.Orders;
using TMarket.WEB.RequestModels.Products;

namespace TMarket.WEB.MappingProfiles
{
    public class DtoToRequestProfile : Profile
    {
        public DtoToRequestProfile()
        {
            CreateMap<UserDTO, UserRespond>();
            CreateMap<UserRequestCommand, UserDTO>();
            CreateMap<UserUpdateCommand, UserDTO>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Command.Name))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Command.Lastname));

            CreateMap<ProductDTO, ProductRespond>().ForMember(dest => dest.CategoryName, opt 
                => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductRequest, ProductDTO>();

            CreateMap<OrderProductDTO, ProductOrderResponse>();

            CreateMap<OrderDTO, OrderResponse>();

            CreateMap<OrderRequest, OrderServiceModel>().ReverseMap();
            
            CreateMap<ProductOrderRequest, OrderProductServiceModel>();
 
            CreateMap<CartProductDTO, ProductCartResponse>();

            CreateMap<CartDTO, CartResponse>();

            CreateMap<CartRequest, CartServiceModel>().ReverseMap();

            CreateMap<ProductCartRequset, CartProductServiceModel>();

            CreateMap<CategoryDTO, CategoryRespond>();
            CreateMap<CategoryRequestCommand, CategoryDTO>();
            CreateMap<CategoryUpdateCommand, CategoryDTO>().ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.Command.Name));
        }
    }
}
