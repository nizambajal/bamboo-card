using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
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
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Areas.Admin.Models.Reports;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

/// <summary>
/// Represents the order model factory implementation
/// </summary>
public partial class OverridenOrderModelFactory : OrderModelFactory
{
    #region Fields

    protected readonly AddressSettings _addressSettings;
    protected readonly CatalogSettings _catalogSettings;
    protected readonly CurrencySettings _currencySettings;
    protected readonly IActionContextAccessor _actionContextAccessor;
    protected readonly IAddressModelFactory _addressModelFactory;
    protected readonly IAddressService _addressService;
    protected readonly IAffiliateService _affiliateService;
    protected readonly IBaseAdminModelFactory _baseAdminModelFactory;
    protected readonly ICountryService _countryService;
    protected readonly ICurrencyService _currencyService;
    protected readonly ICustomerService _customerService;
    protected readonly IDateTimeHelper _dateTimeHelper;
    protected readonly IDiscountService _discountService;
    protected readonly IDownloadService _downloadService;
    protected readonly IEncryptionService _encryptionService;
    protected readonly IGiftCardService _giftCardService;
    protected readonly ILocalizationService _localizationService;
    protected readonly IMeasureService _measureService;
    protected readonly IOrderProcessingService _orderProcessingService;
    protected readonly IOrderReportService _orderReportService;
    protected readonly IOrderService _orderService;
    protected readonly IPaymentPluginManager _paymentPluginManager;
    protected readonly IPaymentService _paymentService;
    protected readonly IPictureService _pictureService;
    protected readonly IPriceCalculationService _priceCalculationService;
    protected readonly IPriceFormatter _priceFormatter;
    protected readonly IProductAttributeService _productAttributeService;
    protected readonly IProductService _productService;
    protected readonly IReturnRequestService _returnRequestService;
    protected readonly IRewardPointService _rewardPointService;
    protected readonly ISettingService _settingService;
    protected readonly IShipmentService _shipmentService;
    protected readonly IShippingService _shippingService;
    protected readonly IStateProvinceService _stateProvinceService;
    protected readonly IStoreService _storeService;
    protected readonly ITaxService _taxService;
    protected readonly IUrlHelperFactory _urlHelperFactory;
    protected readonly IVendorService _vendorService;
    protected readonly IWorkContext _workContext;
    protected readonly MeasureSettings _measureSettings;
    protected readonly NopHttpClient _nopHttpClient;
    protected readonly OrderSettings _orderSettings;
    protected readonly ShippingSettings _shippingSettings;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly TaxSettings _taxSettings;
    private static readonly char[] _separator = [','];

    #endregion

    #region Ctor

    public OverridenOrderModelFactory(AddressSettings addressSettings,
        CatalogSettings catalogSettings,
        CurrencySettings currencySettings,
        IActionContextAccessor actionContextAccessor,
        IAddressModelFactory addressModelFactory,
        IAddressService addressService,
        IAffiliateService affiliateService,
        IBaseAdminModelFactory baseAdminModelFactory,
        ICountryService countryService,
        ICurrencyService currencyService,
        ICustomerService customerService,
        IDateTimeHelper dateTimeHelper,
        IDiscountService discountService,
        IDownloadService downloadService,
        IEncryptionService encryptionService,
        IGiftCardService giftCardService,
        ILocalizationService localizationService,
        IMeasureService measureService,
        IOrderProcessingService orderProcessingService,
        IOrderReportService orderReportService,
        IOrderService orderService,
        IPaymentPluginManager paymentPluginManager,
        IPaymentService paymentService,
        IPictureService pictureService,
        IPriceCalculationService priceCalculationService,
        IPriceFormatter priceFormatter,
        IProductAttributeService productAttributeService,
        IProductService productService,
        IReturnRequestService returnRequestService,
        IRewardPointService rewardPointService,
        ISettingService settingService,
        IShipmentService shipmentService,
        IShippingService shippingService,
        IStateProvinceService stateProvinceService,
        IStoreService storeService,
        ITaxService taxService,
        IUrlHelperFactory urlHelperFactory,
        IVendorService vendorService,
        IWorkContext workContext,
        MeasureSettings measureSettings,
        NopHttpClient nopHttpClient,
        OrderSettings orderSettings,
        ShippingSettings shippingSettings,
        IUrlRecordService urlRecordService,
        TaxSettings taxSettings) : base(addressSettings,
            catalogSettings,
            currencySettings,
            actionContextAccessor,
            addressModelFactory,
            addressService,
            affiliateService,
            baseAdminModelFactory,
            countryService,
            currencyService,
            customerService,
            dateTimeHelper,
            discountService,
            downloadService,
            encryptionService,
            giftCardService,
            localizationService,
            measureService,
            orderProcessingService,
            orderReportService,
            orderService,
            paymentPluginManager,
            paymentService,
            pictureService,
            priceCalculationService,
            priceFormatter,
            productAttributeService,
            productService,
            returnRequestService,
            rewardPointService,
            settingService,
            shipmentService,
            shippingService,
            stateProvinceService,
            storeService,
            taxService,
            urlHelperFactory,
            vendorService,
            workContext,
            measureSettings,
            nopHttpClient,
            orderSettings,
            shippingSettings,
            urlRecordService,
            taxSettings)
    {
        _addressSettings = addressSettings;
        _catalogSettings = catalogSettings;
        _currencySettings = currencySettings;
        _actionContextAccessor = actionContextAccessor;
        _addressModelFactory = addressModelFactory;
        _addressService = addressService;
        _affiliateService = affiliateService;
        _baseAdminModelFactory = baseAdminModelFactory;
        _countryService = countryService;
        _currencyService = currencyService;
        _customerService = customerService;
        _dateTimeHelper = dateTimeHelper;
        _discountService = discountService;
        _downloadService = downloadService;
        _encryptionService = encryptionService;
        _giftCardService = giftCardService;
        _localizationService = localizationService;
        _measureService = measureService;
        _orderProcessingService = orderProcessingService;
        _orderReportService = orderReportService;
        _orderService = orderService;
        _paymentPluginManager = paymentPluginManager;
        _paymentService = paymentService;
        _pictureService = pictureService;
        _priceCalculationService = priceCalculationService;
        _priceFormatter = priceFormatter;
        _productAttributeService = productAttributeService;
        _productService = productService;
        _returnRequestService = returnRequestService;
        _rewardPointService = rewardPointService;
        _settingService = settingService;
        _shipmentService = shipmentService;
        _shippingService = shippingService;
        _stateProvinceService = stateProvinceService;
        _storeService = storeService;
        _taxService = taxService;
        _urlHelperFactory = urlHelperFactory;
        _vendorService = vendorService;
        _workContext = workContext;
        _measureSettings = measureSettings;
        _nopHttpClient = nopHttpClient;
        _orderSettings = orderSettings;
        _shippingSettings = shippingSettings;
        _urlRecordService = urlRecordService;
        _taxSettings = taxSettings;
    }

    #endregion
    
    #region Methods

    /// <summary>
    /// Prepare order model
    /// </summary>
    /// <param name="model">Order model</param>
    /// <param name="order">Order</param>
    /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the order model
    /// </returns>
    public override async Task<OrderModel> PrepareOrderModelAsync(OrderModel model, Order order, bool excludeProperties = false)
    {
        if (order != null)
        {
            //fill in model values from the entity
            model ??= new OrderModel
            {
                Id = order.Id,
                OrderStatusId = order.OrderStatusId,
                VatNumber = order.VatNumber,
                CheckoutAttributeInfo = order.CheckoutAttributeDescription
            };

            var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);

            model.OrderGuid = order.OrderGuid;
            model.CustomOrderNumber = order.CustomOrderNumber;
            model.CustomerIp = order.CustomerIp;
            model.CustomerId = customer.Id;
            model.OrderStatus = await _localizationService.GetLocalizedEnumAsync(order.OrderStatus);
            model.StoreName = (await _storeService.GetStoreByIdAsync(order.StoreId))?.Name ?? "Deleted";
            model.CustomerInfo = await _customerService.IsRegisteredAsync(customer) ? customer.Email : await _localizationService.GetResourceAsync("Admin.Customers.Guest");
            model.CreatedOn = await _dateTimeHelper.ConvertToUserTimeAsync(order.CreatedOnUtc, DateTimeKind.Utc);
            model.CustomValues = _paymentService.DeserializeCustomValues(order);

            // gift message
            model.GiftMessage = order.GiftMessage;

            var affiliate = await _affiliateService.GetAffiliateByIdAsync(order.AffiliateId);
            if (affiliate != null)
            {
                model.AffiliateId = affiliate.Id;
                model.AffiliateName = await _affiliateService.GetAffiliateFullNameAsync(affiliate);
            }

            //prepare order totals
            await PrepareOrderModelTotalsAsync(model, order);

            //prepare order items
            await PrepareOrderItemModelsAsync(model.Items, order);
            model.HasDownloadableProducts = model.Items.Any(item => item.IsDownload);

            //prepare payment info
            await PrepareOrderModelPaymentInfoAsync(model, order);

            //prepare shipping info
            await PrepareOrderModelShippingInfoAsync(model, order);

            //prepare nested search model
            PrepareOrderShipmentSearchModel(model.OrderShipmentSearchModel, order);
            PrepareOrderNoteSearchModel(model.OrderNoteSearchModel, order);
        }

        model.IsLoggedInAsVendor = await _workContext.GetCurrentVendorAsync() != null;
        model.AllowCustomersToSelectTaxDisplayType = _taxSettings.AllowCustomersToSelectTaxDisplayType;
        model.TaxDisplayType = _taxSettings.TaxDisplayType;

        return model;
    }
        
    #endregion
}