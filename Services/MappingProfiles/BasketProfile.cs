using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, BasketDto>().ForMember(d => d.DeliveryMethodId, options => options.MapFrom(s => s.DelivaryMethodId)).ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ForMember(d => d.ProductName, options => options.MapFrom(s => s.Name)).ReverseMap();

        }
    }
}
