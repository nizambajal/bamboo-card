using Nop.Core.Domain.Common;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;

namespace Nop.Core.Domain.Orders;

/// <summary>
/// Represents an order
/// </summary>
public partial class Order
{
    public string GiftMessage { get; set; }
}