using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Services
{
    public interface IOrderApiService
    {
        Task<IPagedList<Order>> SearchOrdersAsync(string email, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
    }
}
