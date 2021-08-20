using AutoMapper;
using Inventory.Domain;
using Inventory.Infrastructure.Data.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductEntity>().ReverseMap();
        }
    }
}
