using System.Collections.Generic;
using System.Globalization;
using DocumentFormat.OpenXml.Drawing;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Data;
using Nop.Plugin.DiscountRules.CustomDiscounts;
using Nop.Services.Attributes;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Payments;
using Nop.Services.Shipping;
using Nop.Services.Tax;

namespace Nop.Services.Orders;

/// <summary>
/// Order service
/// </summary>
public partial class OverridenOrderTotalCalculationService : OrderTotalCalculationService
{
    #region Fields

    protected readonly CatalogSettings _catalogSettings;
    protected readonly IAddressService _addressService;
    protected readonly IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeParser;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly IGiftCardService _giftCardService;
    protected readonly IOrderService _orderService;
    protected readonly IPaymentService _paymentService;
    protected readonly IPriceCalculationService _priceCalculationService;
    protected readonly IProductService _productService;
    protected readonly IRewardPointService _rewardPointService;
    protected readonly IShippingPluginManager _shippingPluginManager;
    protected readonly IShippingService _shippingService;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IStoreContext _storeContext;
    protected readonly ITaxService _taxService;
    protected readonly IWorkContext _workContext;
    protected readonly RewardPointsSettings _rewardPointsSettings;
    protected readonly ShippingSettings _shippingSettings;
    protected readonly ShoppingCartSettings _shoppingCartSettings;
    protected readonly TaxSettings _taxSettings;
    protected readonly ISettingService _settingService;
    protected readonly DiscountSettings _discountSettings;
    protected readonly IRepository<Order> _orderRespository;

    #endregion

    #region Ctor

    public OverridenOrderTotalCalculationService(CatalogSettings catalogSettings,
        IAddressService addressService,
        IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeParser,
        ICustomerService customerService,
        IDiscountService discountService,
        IGenericAttributeService genericAttributeService,
        IGiftCardService giftCardService,
        IOrderService orderService,
        IPaymentService paymentService,
        IPriceCalculationService priceCalculationService,
        IProductService productService,
        IRewardPointService rewardPointService,
        IShippingPluginManager shippingPluginManager,
        IShippingService shippingService,
        IShoppingCartService shoppingCartService,
        IStoreContext storeContext,
        ITaxService taxService,
        IWorkContext workContext,
        RewardPointsSettings rewardPointsSettings,
        ShippingSettings shippingSettings,
        ShoppingCartSettings shoppingCartSettings,
        TaxSettings taxSettings,
        ISettingService settingService,
        DiscountSettings discountSettings,
        IRepository<Order> orderRespository) : base(catalogSettings,
addressService,
checkoutAttributeParser,
customerService,
discountService,
genericAttributeService,
giftCardService,
orderService,
paymentService,
priceCalculationService,
productService,
rewardPointService,
shippingPluginManager,
shippingService,
shoppingCartService,
storeContext,
taxService,
workContext,
rewardPointsSettings,
shippingSettings,
shoppingCartSettings,
taxSettings)
    {
        _catalogSettings = catalogSettings;
        _addressService = addressService;
        _checkoutAttributeParser = checkoutAttributeParser;
        _customerService = customerService;
        _discountService = discountService;
        _genericAttributeService = genericAttributeService;
        _giftCardService = giftCardService;
        _orderService = orderService;
        _paymentService = paymentService;
        _priceCalculationService = priceCalculationService;
        _productService = productService;
        _rewardPointService = rewardPointService;
        _shippingPluginManager = shippingPluginManager;
        _shippingService = shippingService;
        _shoppingCartService = shoppingCartService;
        _storeContext = storeContext;
        _taxService = taxService;
        _workContext = workContext;
        _rewardPointsSettings = rewardPointsSettings;
        _shippingSettings = shippingSettings;
        _shoppingCartSettings = shoppingCartSettings;
        _taxSettings = taxSettings;
        _settingService = settingService;
        _discountSettings = discountSettings;
        _orderRespository = orderRespository;
    }

    #endregion

    #region Utilities

    protected virtual async Task<bool> IsCustomerEligibleForDiscountAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var orderCount = await _orderRespository.Table
                     .Where(o => o.CustomerId == customer.Id)
                     .CountAsync();

        return orderCount >= 3;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets an order discount (applied to order total)
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="orderTotal">Order total</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the order discount. Applied discounts
    /// </returns>
    protected override async Task<(decimal orderDiscount, List<Discount> appliedDiscounts)> GetOrderTotalDiscountAsync(Customer customer, decimal orderTotal)
    {
        var discountAmount = decimal.Zero;
        if (_catalogSettings.IgnoreDiscounts)
            return (discountAmount, new List<Discount>());

        IList<Discount> allDiscounts = new List<Discount>();
        var allowedDiscounts = new List<Discount>();

        if (_discountSettings.Enabled && await IsCustomerEligibleForDiscountAsync(customer))
        {
            // Prepare discount object.
            var discount = new Discount
            {
                Name = "Custom Discount",
                AdminComment = "Applied to the customer having 3 or more orders",
                DiscountTypeId = (int)DiscountType.AssignedToOrderTotal,
                UsePercentage = true,
                DiscountPercentage = _discountSettings.DiscountPercentage, // Can be set from configuration page.
                                                                            //StartDateUtc = DateTime.UtcNow,
                                                                            //EndDateUtc = DateTime.UtcNow.AddDays(1),
                RequiresCouponCode = false,
                DiscountLimitationId = (int)DiscountLimitationType.Unlimited,
                IsActive = true
            };
            allowedDiscounts.Add(discount);
        }
        else
        {
            allDiscounts = await _discountService.GetAllDiscountsAsync(DiscountType.AssignedToOrderTotal);

            if (allDiscounts?.Any() == true)
            {
                var couponCodesToValidate = await _customerService.ParseAppliedDiscountCouponCodesAsync(customer);
                foreach (var discount in allDiscounts)
                {
                    if (!_discountService.ContainsDiscount(allowedDiscounts, discount) &&
                        (await _discountService.ValidateDiscountAsync(discount, customer, couponCodesToValidate)).IsValid)
                    {
                        allowedDiscounts.Add(discount);
                    }
                }
            }
        }

        var appliedDiscounts = _discountService.GetPreferredDiscount(allowedDiscounts, orderTotal, out discountAmount);

        if (discountAmount < decimal.Zero)
            discountAmount = decimal.Zero;

        if (_shoppingCartSettings.RoundPricesDuringCalculation)
            discountAmount = await _priceCalculationService.RoundPriceAsync(discountAmount);

        return (discountAmount, appliedDiscounts);
    }


    #endregion
}