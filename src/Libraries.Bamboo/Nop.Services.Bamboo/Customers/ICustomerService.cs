using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Customers;

namespace Nop.Services.Customers
{
    public partial interface ICustomerService
    {
        /// <summary>
        /// Adds a gift message
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="giftMessage">Gift message</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the updated XML document
        /// </returns>
        Task ApplyGiftMessageAsync(Customer customer, string giftMessage);
    }
}
