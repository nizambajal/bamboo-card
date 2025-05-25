using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Data;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Services
{
    public class OrderApiService : IOrderApiService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Address> _addressRepository;

        public OrderApiService(IRepository<Order> orderRepository, 
            IRepository<Address> addressRepository)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
        }

        public async Task<IPagedList<Order>> SearchOrdersAsync(string email, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = _orderRepository.Table;

            query = from o in query
                    join c in _addressRepository.Table on o.BillingAddressId equals c.Id
                    where c.Email == email
                    select o;

            query = query.Where(o => !o.Deleted);
            query = query.OrderByDescending(o => o.CreatedOnUtc);

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }
    }
}
