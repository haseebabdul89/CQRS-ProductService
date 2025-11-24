using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Commands;
using Product.Application.Interfaces;

namespace Product.Application.Handlers
{
    public class UpdateProductInventoryHandler : IRequestHandler<UpdateProductInventoryCommand, bool>
    {
        private readonly IProductRepository _repository;

        public UpdateProductInventoryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProductInventoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null) return false;

            product.Inventory -= request.Quantity;

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
//    public class UpdateProductInventoryHandler : IRequestHandler<UpdateProductInventoryCommand, bool>
//    {
//        private readonly ProductDbContext _db;

//        public UpdateProductInventoryHandler(ProductDbContext db)
//        {
//            _db = db;
//        }

//        public async Task<bool> Handle(UpdateProductInventoryCommand request, CancellationToken cancellationToken)
//        {
//            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
//            if (product == null) return false;

//            product.Inventory -= request.Quantity; // deduct inventory
//            await _db.SaveChangesAsync(cancellationToken);

//            return true;
//        }
//    }
//}
