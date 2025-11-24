using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Infra.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infra.Persistance.Reposetories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _db;

        public ProductRepository(ProductDbContext db)
        {
            _db = db;
        }

        public async Task<Domain.Entities.Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
