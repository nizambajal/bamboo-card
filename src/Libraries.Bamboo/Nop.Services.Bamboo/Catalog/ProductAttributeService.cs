using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Data;

namespace Nop.Services.Catalog;

/// <summary>
/// Product attribute service
/// </summary>
public partial class ProductAttributeService : IProductAttributeService
{
    #region Methods

    /// <summary>
    /// Gets all product attributes
    /// </summary>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the product attributes
    /// </returns>
    public virtual async Task<IPagedList<ProductAttribute>> GetAllProductAttributesExtendedAsync(string productAttributeName = null,
        int pageIndex = 0,
        int pageSize = int.MaxValue)
    {
        var productAttributes = await _productAttributeRepository.GetAllPagedAsync(query =>
        {
            if (!string.IsNullOrEmpty(productAttributeName))
            {
                query = query.Where(pa => pa.Name.Contains(productAttributeName));
            }

            return from pa in query
                orderby pa.Name
                select pa;
        }, pageIndex, pageSize);

        return productAttributes;
    }

    #endregion
}