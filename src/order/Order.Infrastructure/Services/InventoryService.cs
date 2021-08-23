using Microsoft.Extensions.Configuration;
using MS.Communication;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly string _baseAddress;
        private readonly ICommunicationProvider _communicationProvider;
        private readonly IConfiguration _configuration;

        public InventoryService(string baseAddress, 
            ICommunicationProvider communicationProvider, IConfiguration configuration)
        {
            this._baseAddress = baseAddress;
            this._communicationProvider = communicationProvider;
            this._configuration = configuration;
        }
        public async Task<IEnumerable<Product>> GetAsync() {
            var url = $"{_baseAddress}/api/v1/Products";
            var productList = await _communicationProvider.SendAsync<IList<Product>, object>(url, HttpMethod.Get,null);
            return productList;
        }
    }
}
