using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Domain.Models;
using Order.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<OrderController> _logger;
        private readonly IInventoryService _inventoryService;

        public OrderController(ILogger<OrderController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            this._inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAsync()
        {
      
           return await  _inventoryService.GetAsync();
        }
    }
}
