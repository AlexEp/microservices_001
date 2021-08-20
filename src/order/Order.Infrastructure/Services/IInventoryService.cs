using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public interface  IInventoryService
    {
        public Task<IEnumerable<Product>> GetAsync();
    }
}
