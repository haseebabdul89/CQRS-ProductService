using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product.Domain.Entities.Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
