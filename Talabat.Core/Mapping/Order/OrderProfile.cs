using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.Orders;
using Talabat.Core.Entities.Orders;

namespace Talabat.Core.Mapping.Order
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl));
            CreateMap<DeliveryMethod, DeliveryMethodResult>().ReverseMap();
            CreateMap<Talabat.Core.Entities.Orders.Order, OrderResult>()
              .ForMember(dest => dest.PaymentStatus, options => options.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, options => options.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));

        }
    }
}
