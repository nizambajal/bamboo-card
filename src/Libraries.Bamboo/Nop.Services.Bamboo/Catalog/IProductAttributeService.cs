using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Catalog;
using Nop.Core;

namespace Nop.Services.Catalog
{
    public partial interface IProductAttributeService
    {
        /// <summary>
        /// Gets all product attributes
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product attributes
        /// </returns>
        Task<IPagedList<ProductAttribute>> GetAllProductAttributesExtendedAsync(string productAttributeName = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue);
    }
}
