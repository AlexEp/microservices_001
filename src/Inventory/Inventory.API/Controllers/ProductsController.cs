using Inventory.Domain;
using Inventory.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration configuration;

        public ProductsController(ILogger<ProductsController> logger, 
            IProductRepository productRepository, IConfiguration configuration)
        {
            _logger = logger;
            _productRepository = productRepository;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAllAsync();
        }


    }
}
