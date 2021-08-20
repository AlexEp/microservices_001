using Order.Domain.Models;
using Order.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly string _baseAddress;
        private readonly ICommunicationProvider _communicationProvider;

        public InventoryService(string baseAddress, ICommunicationProvider communicationProvider)
        {
            this._baseAddress = baseAddress;
            this._communicationProvider = communicationProvider;
        }
        public async Task<IEnumerable<Product>> GetAsync() {
            var url = $"{_baseAddress}/api/v1/Products";
            var productList = await _communicationProvider.SendAsync<IList<Product>, object>(url, HttpMethod.Get,null);
            return productList;

        }
    }
}
