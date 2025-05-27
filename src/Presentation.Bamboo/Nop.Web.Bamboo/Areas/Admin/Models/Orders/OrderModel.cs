using System.ComponentModel.DataAnnotations;
using Nop.Core.Domain.Tax;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Orders;

/// <summary>
/// Represents an order model
/// </summary>
public partial record OrderModel
{
    public string GiftMessage { get; set; }
}