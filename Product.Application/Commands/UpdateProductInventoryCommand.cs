using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.Commands
{
   public record UpdateProductInventoryCommand(Guid ProductId, int Quantity, string Reason) :IRequest<bool>;
}
