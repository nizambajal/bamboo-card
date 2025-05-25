using System.Globalization;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Events;
using Nop.Data;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;

namespace Nop.Services.Orders;

/// <summary>
/// Order processing service
/// </summary>
public partial class OverridenOrderProcessingService : OrderProcessingService
{
    #region Fields

    protected readonly CurrencySettings _currencySettings;
    protected readonly IAddressService _addressService;
    protected readonly IAffiliateService _affiliateService;
    protected readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
    protected readonly ICountryService _countryService;
    protected readonly ICurrencyService _currencyService;
    protected readonly ICustomerActivityService _customerActivityService;
    protected readonly ICustomerService _customerService;
    protected readonly ICustomNumberFormatter _customNumberFormatter;
    protected readonly IDiscountService _discountService;
    protected readonly IEncryptionService _encryptionService;
    protected readonly IEventPublisher _eventPublisher;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly IGiftCardService _giftCardService;
    protected readonly ILanguageService _languageService;
    protected readonly ILocalizationService _localizationService;
    protected readonly ILogger _logger;
    protected readonly IOrderService _orderService;
    protected readonly IOrderTotalCalculationService _orderTotalCalculationService;
    protected readonly IPaymentPluginManager _paymentPluginManager;
    protected readonly IPaymentService _paymentService;
    protected readonly IPdfService _pdfService;
    protected readonly IPriceCalculationService _priceCalculationService;
    protected readonly IPriceFormatter _priceFormatter;
    protected readonly IProductAttributeFormatter _productAttributeFormatter;
    protected readonly IProductAttributeParser _productAttributeParser;
    protected readonly IProductService _productService;
    protected readonly IReturnRequestService _returnRequestService;
    protected readonly IRewardPointService _rewardPointService;
    protected readonly IShipmentService _shipmentService;
    protected readonly IShippingService _shippingService;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IStateProvinceService _stateProvinceService;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly IStoreMappingService _storeMappingService;
    protected readonly IStoreService _storeService;
    protected readonly ITaxService _taxService;
    protected readonly IVendorService _vendorService;
    protected readonly IWebHelper _webHelper;
    protected readonly IWorkContext _workContext;
    protected readonly IWorkflowMessageService _workflowMessageService;
    protected readonly LocalizationSettings _localizationSettings;
    protected readonly OrderSettings _orderSettings;
    protected readonly PaymentSettings _paymentSettings;
    protected readonly RewardPointsSettings _rewardPointsSettings;
    protected readonly ShippingSettings _shippingSettings;
    protected readonly TaxSettings _taxSettings;
    protected readonly IRepository<GenericAttribute> _genericAttributeRepository;

    #endregion

    #region Ctor

    public OverridenOrderProcessingService(CurrencySettings currencySettings,
        IAddressService addressService,
        IAffiliateService affiliateService,
        ICheckoutAttributeFormatter checkoutAttributeFormatter,
        ICountryService countryService,
        ICurrencyService currencyService,
        ICustomerActivityService customerActivityService,
        ICustomerService customerService,
        ICustomNumberFormatter customNumberFormatter,
        IDiscountService discountService,
        IEncryptionService encryptionService,
        IEventPublisher eventPublisher,
        IGenericAttributeService genericAttributeService,
        IGiftCardService giftCardService,
        ILanguageService languageService,
        ILocalizationService localizationService,
        ILogger logger,
        IOrderService orderService,
        IOrderTotalCalculationService orderTotalCalculationService,
        IPaymentPluginManager paymentPluginManager,
        IPaymentService paymentService,
        IPdfService pdfService,
        IPriceCalculationService priceCalculationService,
        IPriceFormatter priceFormatter,
        IProductAttributeFormatter productAttributeFormatter,
        IProductAttributeParser productAttributeParser,
        IProductService productService,
        IReturnRequestService returnRequestService,
        IRewardPointService rewardPointService,
        IShipmentService shipmentService,
        IShippingService shippingService,
        IShoppingCartService shoppingCartService,
        IStateProvinceService stateProvinceService,
        IStaticCacheManager staticCacheManager,
        IStoreMappingService storeMappingService,
        IStoreService storeService,
        ITaxService taxService,
        IVendorService vendorService,
        IWebHelper webHelper,
        IWorkContext workContext,
        IWorkflowMessageService workflowMessageService,
        LocalizationSettings localizationSettings,
        OrderSettings orderSettings,
        PaymentSettings paymentSettings,
        RewardPointsSettings rewardPointsSettings,
        ShippingSettings shippingSettings,
        TaxSettings taxSettings,
        IRepository<GenericAttribute> genericAttributeRepository) : base(currencySettings,
            addressService,
            affiliateService,
            checkoutAttributeFormatter,
            countryService,
            currencyService,
            customerActivityService,
            customerService,
            customNumberFormatter,
            discountService,
            encryptionService,
            eventPublisher,
            genericAttributeService,
            giftCardService,
            languageService,
            localizationService,
            logger,
            orderService,
            orderTotalCalculationService,
            paymentPluginManager,
            paymentService,
            pdfService,
            priceCalculationService,
            priceFormatter,
            productAttributeFormatter,
            productAttributeParser,
            productService,
            returnRequestService,
            rewardPointService,
            shipmentService,
            shippingService,
            shoppingCartService,
            stateProvinceService,
            staticCacheManager,
            storeMappingService,
            storeService,
            taxService,
            vendorService,
            webHelper,
            workContext,
            workflowMessageService,
            localizationSettings,
            orderSettings,
            paymentSettings,
            rewardPointsSettings,
            shippingSettings,
            taxSettings)
    {
        _currencySettings = currencySettings;
        _addressService = addressService;
        _affiliateService = affiliateService;
        _checkoutAttributeFormatter = checkoutAttributeFormatter;
        _countryService = countryService;
        _currencyService = currencyService;
        _customerActivityService = customerActivityService;
        _customerService = customerService;
        _customNumberFormatter = customNumberFormatter;
        _discountService = discountService;
        _encryptionService = encryptionService;
        _eventPublisher = eventPublisher;
        _genericAttributeService = genericAttributeService;
        _giftCardService = giftCardService;
        _languageService = languageService;
        _localizationService = localizationService;
        _logger = logger;
        _orderService = orderService;
        _orderTotalCalculationService = orderTotalCalculationService;
        _paymentPluginManager = paymentPluginManager;
        _paymentService = paymentService;
        _pdfService = pdfService;
        _priceCalculationService = priceCalculationService;
        _priceFormatter = priceFormatter;
        _productAttributeFormatter = productAttributeFormatter;
        _productAttributeParser = productAttributeParser;
        _productService = productService;
        _returnRequestService = returnRequestService;
        _rewardPointService = rewardPointService;
        _shipmentService = shipmentService;
        _shippingService = shippingService;
        _shoppingCartService = shoppingCartService;
        _stateProvinceService = stateProvinceService;
        _staticCacheManager = staticCacheManager;
        _storeMappingService = storeMappingService;
        _storeService = storeService;
        _taxService = taxService;
        _vendorService = vendorService;
        _webHelper = webHelper;
        _workContext = workContext;
        _workflowMessageService = workflowMessageService;
        _localizationSettings = localizationSettings;
        _orderSettings = orderSettings;
        _paymentSettings = paymentSettings;
        _rewardPointsSettings = rewardPointsSettings;
        _shippingSettings = shippingSettings;
        _taxSettings = taxSettings;
        _genericAttributeRepository = genericAttributeRepository;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Save order and add order notes
    /// </summary>
    /// <param name="processPaymentRequest">Process payment request</param>
    /// <param name="processPaymentResult">Process payment result</param>
    /// <param name="details">Details</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the order
    /// </returns>
    protected override async Task<Order> SaveOrderDetailsAsync(ProcessPaymentRequest processPaymentRequest,
        ProcessPaymentResult processPaymentResult, PlaceOrderContainer details)
    {
        //var giftMessage = await _genericAttributeService.GetAttributeAsync<string>(details.Customer, NopCustomerDefaults.GiftMessageAttribute);

        var attributes = await _genericAttributeService.GetAttributesForEntityAsync(details.Customer.Id, nameof(Customer));
        var attribute = attributes.FirstOrDefault(x => x.Key == NopCustomerDefaults.GiftMessageAttribute);

        var order = new Order
        {
            StoreId = processPaymentRequest.StoreId,
            OrderGuid = processPaymentRequest.OrderGuid,
            CustomerId = details.Customer.Id,
            CustomerLanguageId = details.CustomerLanguage.Id,
            CustomerTaxDisplayType = details.CustomerTaxDisplayType,
            CustomerIp = _webHelper.GetCurrentIpAddress(),
            OrderSubtotalInclTax = details.OrderSubTotalInclTax,
            OrderSubtotalExclTax = details.OrderSubTotalExclTax,
            OrderSubTotalDiscountInclTax = details.OrderSubTotalDiscountInclTax,
            OrderSubTotalDiscountExclTax = details.OrderSubTotalDiscountExclTax,
            OrderShippingInclTax = details.OrderShippingTotalInclTax,
            OrderShippingExclTax = details.OrderShippingTotalExclTax,
            PaymentMethodAdditionalFeeInclTax = details.PaymentAdditionalFeeInclTax,
            PaymentMethodAdditionalFeeExclTax = details.PaymentAdditionalFeeExclTax,
            TaxRates = details.TaxRates,
            OrderTax = details.OrderTaxTotal,
            OrderTotal = details.OrderTotal,
            RefundedAmount = decimal.Zero,
            OrderDiscount = details.OrderDiscountAmount,
            CheckoutAttributeDescription = details.CheckoutAttributeDescription,
            CheckoutAttributesXml = details.CheckoutAttributesXml,
            CustomerCurrencyCode = details.CustomerCurrencyCode,
            CurrencyRate = details.CustomerCurrencyRate,
            AffiliateId = details.AffiliateId,
            OrderStatus = OrderStatus.Pending,
            AllowStoringCreditCardNumber = processPaymentResult.AllowStoringCreditCardNumber,
            CardType = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardType) : string.Empty,
            CardName = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardName) : string.Empty,
            CardNumber = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardNumber) : string.Empty,
            MaskedCreditCardNumber = _encryptionService.EncryptText(_paymentService.GetMaskedCreditCardNumber(processPaymentRequest.CreditCardNumber)),
            CardCvv2 = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardCvv2) : string.Empty,
            CardExpirationMonth = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardExpireMonth.ToString()) : string.Empty,
            CardExpirationYear = processPaymentResult.AllowStoringCreditCardNumber ? _encryptionService.EncryptText(processPaymentRequest.CreditCardExpireYear.ToString()) : string.Empty,
            PaymentMethodSystemName = processPaymentRequest.PaymentMethodSystemName,
            AuthorizationTransactionId = processPaymentResult.AuthorizationTransactionId,
            AuthorizationTransactionCode = processPaymentResult.AuthorizationTransactionCode,
            AuthorizationTransactionResult = processPaymentResult.AuthorizationTransactionResult,
            CaptureTransactionId = processPaymentResult.CaptureTransactionId,
            CaptureTransactionResult = processPaymentResult.CaptureTransactionResult,
            SubscriptionTransactionId = processPaymentResult.SubscriptionTransactionId,
            PaymentStatus = processPaymentResult.NewPaymentStatus,
            PaidDateUtc = null,
            PickupInStore = details.PickupInStore,
            ShippingStatus = details.ShippingStatus,
            ShippingMethod = details.ShippingMethodName,
            ShippingRateComputationMethodSystemName = details.ShippingRateComputationMethodSystemName,
            CustomValuesXml = _paymentService.SerializeCustomValues(processPaymentRequest),
            VatNumber = details.VatNumber,
            CreatedOnUtc = DateTime.UtcNow,
            CustomOrderNumber = string.Empty,
            GiftMessage = attribute?.Value
        };

        if (details.BillingAddress is null)
            throw new NopException("Billing address is not provided");

        await _addressService.InsertAddressAsync(details.BillingAddress);
        order.BillingAddressId = details.BillingAddress.Id;

        if (details.PickupAddress != null)
        {
            await _addressService.InsertAddressAsync(details.PickupAddress);
            order.PickupAddressId = details.PickupAddress.Id;
        }

        if (details.ShippingAddress != null)
        {
            await _addressService.InsertAddressAsync(details.ShippingAddress);
            order.ShippingAddressId = details.ShippingAddress.Id;
        }

        await _orderService.InsertOrderAsync(order);

        //generate and set custom order number
        order.CustomOrderNumber = _customNumberFormatter.GenerateOrderCustomNumber(order);
        await _orderService.UpdateOrderAsync(order);

        // gift message must be removed from ga.
        //var attribute = from ga in _genericAttributeRepository.Table
        //                where ga.KeyGroup.Equals(nameof(Customer)) && ga.Key.Equals(NopCustomerDefaults.GiftMessageAttribute) && ga.EntityId == details.Customer.Id
        //                select ga;

        

        await _genericAttributeService.DeleteAttributeAsync(attribute);

        //reward points history
        if (details.RedeemedRewardPointsAmount <= decimal.Zero)
            return order;

        order.RedeemedRewardPointsEntryId = await _rewardPointService.AddRewardPointsHistoryEntryAsync(details.Customer, -details.RedeemedRewardPoints, order.StoreId,
            string.Format(await _localizationService.GetResourceAsync("RewardPoints.Message.RedeemedForOrder", order.CustomerLanguageId), order.CustomOrderNumber),
            order, details.RedeemedRewardPointsAmount);

        await _orderService.UpdateOrderAsync(order);

        return order;
    }

    #endregion
}