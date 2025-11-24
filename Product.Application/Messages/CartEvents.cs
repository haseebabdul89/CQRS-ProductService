using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Messages
{
    public record ProductInventoryMessage(Guid ProductId, int Quantity, string Reason);
    public record CartItemRemovedMessage(Guid ProductId, int Quantity, string Reason);
}
