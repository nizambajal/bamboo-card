using System.Xml;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Events;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Common;
using Nop.Services.Localization;

namespace Nop.Services.Customers;

/// <summary>
/// Customer service
/// </summary>
public partial class CustomerService
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
    public virtual async Task ApplyGiftMessageAsync(Customer customer, string giftMessage)
    {
        ArgumentNullException.ThrowIfNull(customer);

        //var result = string.Empty;
        //try
        //{
        //    var existingGiftMessage = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.GiftMessageAttribute);

        //    giftMessage = giftMessage?.Trim();

        //    var xmlDoc = new XmlDocument();
        //    if (string.IsNullOrEmpty(existingGiftMessage))
        //    {
        //        var element1 = xmlDoc.CreateElement("GiftMessages");
        //        xmlDoc.AppendChild(element1);
        //    }
        //    else
        //        xmlDoc.LoadXml(existingGiftMessage);

        //    var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//GiftMessages");

        //    // Directly add the GiftMessage attribute on the root element
        //    if (!string.IsNullOrEmpty(giftMessage))
        //    {
        //        rootElement.SetAttribute("GiftMessage", giftMessage);
        //    }

        //    result = xmlDoc.OuterXml;
        //}
        //catch
        //{
        //    // ignored
        //}

        // Apply the new value
        await _genericAttributeService.SaveAttributeAsync(customer, NopCustomerDefaults.GiftMessageAttribute, giftMessage);
    }

}