using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Http.Extensions;
using Nop.Services.Attributes;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Common;
using Nop.Web.Models.Media;
using Nop.Web.Models.ShoppingCart;

namespace Nop.Web.Factories;

/// <summary>
/// Represents the shopping cart model factory
/// </summary>
public partial class OverridenShoppingCartModelFactory : ShoppingCartModelFactory
{
    #region Fields

    protected readonly AddressSettings _addressSettings;
    protected readonly CaptchaSettings _captchaSettings;
    protected readonly CatalogSettings _catalogSettings;
    protected readonly CommonSettings _commonSettings;
    protected readonly CustomerSettings _customerSettings;
    protected readonly IAddressModelFactory _addressModelFactory;
    protected readonly IAddressService _addressService;
    protected readonly IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeParser;
    protected readonly IAttributeService<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeService;
    protected readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
    protected readonly ICountryService _countryService;
    protected readonly ICurrencyService _currencyService;
    protected readonly ICustomerService _customerService;
    protected readonly IDateTimeHelper _dateTimeHelper;
    protected readonly IDiscountService _discountService;
    protected readonly IDownloadService _downloadService;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly IGiftCardService _giftCardService;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly ILocalizationService _localizationService;
    protected readonly IOrderProcessingService _orderProcessingService;
    protected readonly IOrderTotalCalculationService _orderTotalCalculationService;
    protected readonly IPaymentPluginManager _paymentPluginManager;
    protected readonly IPaymentService _paymentService;
    protected readonly IPermissionService _permissionService;
    protected readonly IPictureService _pictureService;
    protected readonly IPriceFormatter _priceFormatter;
    protected readonly IProductAttributeFormatter _productAttributeFormatter;
    protected readonly IProductService _productService;
    protected readonly IShippingService _shippingService;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IShortTermCacheManager _shortTermCacheManager;
    protected readonly IStateProvinceService _stateProvinceService;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly IStoreContext _storeContext;
    protected readonly IStoreMappingService _storeMappingService;
    protected readonly ITaxService _taxService;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly IVendorService _vendorService;
    protected readonly IWebHelper _webHelper;
    protected readonly IWorkContext _workContext;
    protected readonly MediaSettings _mediaSettings;
    protected readonly OrderSettings _orderSettings;
    protected readonly RewardPointsSettings _rewardPointsSettings;
    protected readonly ShippingSettings _shippingSettings;
    protected readonly ShoppingCartSettings _shoppingCartSettings;
    protected readonly TaxSettings _taxSettings;
    protected readonly VendorSettings _vendorSettings;
    private static readonly char[] _separator = [','];

    #endregion

    #region Ctor

    public OverridenShoppingCartModelFactory(AddressSettings addressSettings,
        CaptchaSettings captchaSettings,
        CatalogSettings catalogSettings,
        CommonSettings commonSettings,
        CustomerSettings customerSettings,
        IAddressModelFactory addressModelFactory,
        IAddressService addressService,
        IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeParser,
        IAttributeService<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeService,
        ICheckoutAttributeFormatter checkoutAttributeFormatter,
        ICountryService countryService,
        ICurrencyService currencyService,
        ICustomerService customerService,
        IDateTimeHelper dateTimeHelper,
        IDiscountService discountService,
        IDownloadService downloadService,
        IGenericAttributeService genericAttributeService,
        IGiftCardService giftCardService,
        IHttpContextAccessor httpContextAccessor,
        ILocalizationService localizationService,
        IOrderProcessingService orderProcessingService,
        IOrderTotalCalculationService orderTotalCalculationService,
        IPaymentPluginManager paymentPluginManager,
        IPaymentService paymentService,
        IPermissionService permissionService,
        IPictureService pictureService,
        IPriceFormatter priceFormatter,
        IProductAttributeFormatter productAttributeFormatter,
        IProductService productService,
        IShippingService shippingService,
        IShoppingCartService shoppingCartService,
        IShortTermCacheManager shortTermCacheManager,
        IStateProvinceService stateProvinceService,
        IStaticCacheManager staticCacheManager,
        IStoreContext storeContext,
        IStoreMappingService storeMappingService,
        ITaxService taxService,
        IUrlRecordService urlRecordService,
        IVendorService vendorService,
        IWebHelper webHelper,
        IWorkContext workContext,
        MediaSettings mediaSettings,
        OrderSettings orderSettings,
        RewardPointsSettings rewardPointsSettings,
        ShippingSettings shippingSettings,
        ShoppingCartSettings shoppingCartSettings,
        TaxSettings taxSettings,
        VendorSettings vendorSettings) : base(addressSettings,
            captchaSettings,
            catalogSettings,
            commonSettings,
            customerSettings,
            addressModelFactory,
            addressService,
            checkoutAttributeParser,
            checkoutAttributeService,
            checkoutAttributeFormatter,
            countryService,
            currencyService,
            customerService,
            dateTimeHelper,
            discountService,
            downloadService,
            genericAttributeService,
            giftCardService,
            httpContextAccessor,
            localizationService,
            orderProcessingService,
            orderTotalCalculationService,
            paymentPluginManager,
            paymentService,
            permissionService,
            pictureService,
            priceFormatter,
            productAttributeFormatter,
            productService,
            shippingService,
            shoppingCartService,
            shortTermCacheManager,
            stateProvinceService,
            staticCacheManager,
            storeContext,
            storeMappingService,
            taxService,
            urlRecordService,
            vendorService,
            webHelper,
            workContext,
            mediaSettings,
            orderSettings,
            rewardPointsSettings,
            shippingSettings,
            shoppingCartSettings,
            taxSettings,
            vendorSettings)
    {
        _addressSettings = addressSettings;
        _addressService = addressService;
        _captchaSettings = captchaSettings;
        _catalogSettings = catalogSettings;
        _commonSettings = commonSettings;
        _customerSettings = customerSettings;
        _addressModelFactory = addressModelFactory;
        _checkoutAttributeParser = checkoutAttributeParser;
        _checkoutAttributeService = checkoutAttributeService;
        _checkoutAttributeFormatter = checkoutAttributeFormatter;
        _countryService = countryService;
        _currencyService = currencyService;
        _customerService = customerService;
        _dateTimeHelper = dateTimeHelper;
        _discountService = discountService;
        _downloadService = downloadService;
        _genericAttributeService = genericAttributeService;
        _giftCardService = giftCardService;
        _httpContextAccessor = httpContextAccessor;
        _localizationService = localizationService;
        _orderProcessingService = orderProcessingService;
        _orderTotalCalculationService = orderTotalCalculationService;
        _paymentPluginManager = paymentPluginManager;
        _paymentService = paymentService;
        _permissionService = permissionService;
        _pictureService = pictureService;
        _priceFormatter = priceFormatter;
        _productAttributeFormatter = productAttributeFormatter;
        _productService = productService;
        _shippingService = shippingService;
        _shoppingCartService = shoppingCartService;
        _shortTermCacheManager = shortTermCacheManager;
        _stateProvinceService = stateProvinceService;
        _staticCacheManager = staticCacheManager;
        _storeContext = storeContext;
        _storeMappingService = storeMappingService;
        _taxService = taxService;
        _urlRecordService = urlRecordService;
        _vendorService = vendorService;
        _webHelper = webHelper;
        _workContext = workContext;
        _mediaSettings = mediaSettings;
        _orderSettings = orderSettings;
        _rewardPointsSettings = rewardPointsSettings;
        _shippingSettings = shippingSettings;
        _shoppingCartSettings = shoppingCartSettings;
        _taxSettings = taxSettings;
        _vendorSettings = vendorSettings;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Prepare the shopping cart model
    /// </summary>
    /// <param name="model">Shopping cart model</param>
    /// <param name="cart">List of the shopping cart item</param>
    /// <param name="isEditable">Whether model is editable</param>
    /// <param name="validateCheckoutAttributes">Whether to validate checkout attributes</param>
    /// <param name="prepareAndDisplayOrderReviewData">Whether to prepare and display order review data</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the shopping cart model
    /// </returns>
    public override async Task<ShoppingCartModel> PrepareShoppingCartModelAsync(ShoppingCartModel model,
        IList<ShoppingCartItem> cart, bool isEditable = true,
        bool validateCheckoutAttributes = false,
        bool prepareAndDisplayOrderReviewData = false)
    {
        ArgumentNullException.ThrowIfNull(cart);

        ArgumentNullException.ThrowIfNull(model);

        //simple properties
        model.OnePageCheckoutEnabled = _orderSettings.OnePageCheckoutEnabled;

        if (!cart.Any())
            return model;

        model.IsEditable = isEditable;
        model.ShowProductImages = _shoppingCartSettings.ShowProductImagesOnShoppingCart;
        model.ShowSku = _catalogSettings.ShowSkuOnProductDetailsPage;
        model.ShowVendorName = _vendorSettings.ShowVendorOnOrderDetailsPage;
        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();
        var checkoutAttributesXml = await _genericAttributeService.GetAttributeAsync<string>(customer,
            NopCustomerDefaults.CheckoutAttributes, store.Id);
        var minOrderSubtotalAmountOk = await _orderProcessingService.ValidateMinOrderSubtotalAmountAsync(cart);
        if (!minOrderSubtotalAmountOk)
        {
            var minOrderSubtotalAmount = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(_orderSettings.MinOrderSubtotalAmount, await _workContext.GetWorkingCurrencyAsync());
            model.MinOrderSubtotalWarning = string.Format(await _localizationService.GetResourceAsync("Checkout.MinOrderSubtotalAmount"), await _priceFormatter.FormatPriceAsync(minOrderSubtotalAmount, true, false));
        }

        model.TermsOfServiceOnShoppingCartPage = _orderSettings.TermsOfServiceOnShoppingCartPage;
        model.TermsOfServiceOnOrderConfirmPage = _orderSettings.TermsOfServiceOnOrderConfirmPage;
        model.TermsOfServicePopup = _commonSettings.PopupForTermsOfServiceLinks;
        model.DisplayTaxShippingInfo = _catalogSettings.DisplayTaxShippingInfoShoppingCart;

        //discount and gift card boxes
        model.DiscountBox.Display = _shoppingCartSettings.ShowDiscountBox;
        var discountCouponCodes = await _customerService.ParseAppliedDiscountCouponCodesAsync(customer);

        foreach (var couponCode in discountCouponCodes)
        {
            var discount = await (await _discountService.GetAllDiscountsAsync(couponCode: couponCode))
                .FirstOrDefaultAwaitAsync(async d => d.RequiresCouponCode && (await _discountService.ValidateDiscountAsync(d, customer, discountCouponCodes)).IsValid);

            if (discount != null)
            {
                model.DiscountBox.AppliedDiscountsWithCodes.Add(new ShoppingCartModel.DiscountBoxModel.DiscountInfoModel
                {
                    Id = discount.Id,
                    CouponCode = discount.CouponCode
                });
            }
        }

        model.GiftCardBox.Display = _shoppingCartSettings.ShowGiftCardBox;

        model.GiftMessage = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.GiftMessageAttribute);

        //cart warnings
        var cartWarnings = await _shoppingCartService.GetShoppingCartWarningsAsync(cart, checkoutAttributesXml, validateCheckoutAttributes);
        foreach (var warning in cartWarnings)
            model.Warnings.Add(warning);

        //checkout attributes
        model.CheckoutAttributes = await PrepareCheckoutAttributeModelsAsync(cart);

        //cart items
        foreach (var sci in cart)
        {
            var cartItemModel = await PrepareShoppingCartItemModelAsync(cart, sci);
            model.Items.Add(cartItemModel);
        }

        //payment methods
        //all payment methods (do not filter by country here as it could be not specified yet)
        var paymentMethods = await (await _paymentPluginManager
                .LoadActivePluginsAsync(customer, store.Id))
            .WhereAwait(async pm => !await pm.HidePaymentMethodAsync(cart)).ToListAsync();
        //payment methods displayed during checkout (not with "Button" type)
        var nonButtonPaymentMethods = paymentMethods
            .Where(pm => pm.PaymentMethodType != PaymentMethodType.Button)
            .ToList();
        //"button" payment methods(*displayed on the shopping cart page)
        var buttonPaymentMethods = paymentMethods
            .Where(pm => pm.PaymentMethodType == PaymentMethodType.Button)
            .ToList();
        foreach (var pm in buttonPaymentMethods)
        {
            if (await _shoppingCartService.ShoppingCartIsRecurringAsync(cart) && pm.RecurringPaymentType == RecurringPaymentType.NotSupported)
                continue;

            var viewComponent = pm.GetPublicViewComponent();
            model.ButtonPaymentMethodViewComponents.Add(viewComponent);
        }
        //hide "Checkout" button if we have only "Button" payment methods
        model.HideCheckoutButton = !nonButtonPaymentMethods.Any() && model.ButtonPaymentMethodViewComponents.Any();

        //order review data
        if (prepareAndDisplayOrderReviewData)
        {
            model.OrderReviewData = await PrepareOrderReviewDataModelAsync(cart);
        }

        return model;
    }


    #endregion
}