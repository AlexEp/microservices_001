using AutoMapper;
using Inventory.Domain;
using Inventory.Infrastructure.Data.Context;
using Inventory.Infrastructure.Data.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Data.Repositories
{
    internal class ProductRepository : RepositoryBase<Product, ProductEntity>, IProductRepository
    {
        public ProductRepository(InventoryContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
