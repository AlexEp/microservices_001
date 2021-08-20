using Inventory.Domain;

namespace Inventory.Infrastructure.Data.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}
