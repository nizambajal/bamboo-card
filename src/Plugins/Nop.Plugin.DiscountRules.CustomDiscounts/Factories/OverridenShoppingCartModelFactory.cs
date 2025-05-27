//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Nop.Core.Caching;
//using Nop.Core.Domain.Catalog;
//using Nop.Core.Domain.Common;
//using Nop.Core.Domain.Customers;
//using Nop.Core.Domain.Media;
//using Nop.Core.Domain.Orders;
//using Nop.Core.Domain.Security;
//using Nop.Core.Domain.Shipping;
//using Nop.Core.Domain.Tax;
//using Nop.Core.Domain.Vendors;
//using Nop.Core;
//using Nop.Services.Attributes;
//using Nop.Services.Catalog;
//using Nop.Services.Common;
//using Nop.Services.Customers;
//using Nop.Services.Directory;
//using Nop.Services.Discounts;
//using Nop.Services.Helpers;
//using Nop.Services.Localization;
//using Nop.Services.Media;
//using Nop.Services.Orders;
//using Nop.Services.Payments;
//using Nop.Services.Security;
//using Nop.Services.Seo;
//using Nop.Services.Shipping;
//using Nop.Services.Stores;
//using Nop.Services.Tax;
//using Nop.Services.Vendors;
//using Nop.Web.Factories;
//using Nop.Web.Models.ShoppingCart;

//namespace Nop.Plugin.DiscountRules.CustomDiscounts.Factories
//{
//    public partial class OverridenShoppingCartModelFactory : ShoppingCartModelFactory
//    {

//        #region Fields

//        protected readonly AddressSettings _addressSettings;
//        protected readonly CaptchaSettings _captchaSettings;
//        protected readonly CatalogSettings _catalogSettings;
//        protected readonly CommonSettings _commonSettings;
//        protected readonly CustomerSettings _customerSettings;
//        protected readonly IAddressModelFactory _addressModelFactory;
//        protected readonly IAddressService _addressService;
//        protected readonly IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeParser;
//        protected readonly IAttributeService<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeService;
//        protected readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
//        protected readonly ICountryService _countryService;
//        protected readonly ICurrencyService _currencyService;
//        protected readonly ICustomerService _customerService;
//        protected readonly IDateTimeHelper _dateTimeHelper;
//        protected readonly IDiscountService _discountService;
//        protected readonly IDownloadService _downloadService;
//        protected readonly IGenericAttributeService _genericAttributeService;
//        protected readonly IGiftCardService _giftCardService;
//        protected readonly IHttpContextAccessor _httpContextAccessor;
//        protected readonly ILocalizationService _localizationService;
//        protected readonly IOrderProcessingService _orderProcessingService;
//        protected readonly IOrderTotalCalculationService _orderTotalCalculationService;
//        protected readonly IPaymentPluginManager _paymentPluginManager;
//        protected readonly IPaymentService _paymentService;
//        protected readonly IPermissionService _permissionService;
//        protected readonly IPictureService _pictureService;
//        protected readonly IPriceFormatter _priceFormatter;
//        protected readonly IProductAttributeFormatter _productAttributeFormatter;
//        protected readonly IProductService _productService;
//        protected readonly IShippingService _shippingService;
//        protected readonly IShoppingCartService _shoppingCartService;
//        protected readonly IShortTermCacheManager _shortTermCacheManager;
//        protected readonly IStateProvinceService _stateProvinceService;
//        protected readonly IStaticCacheManager _staticCacheManager;
//        protected readonly IStoreContext _storeContext;
//        protected readonly IStoreMappingService _storeMappingService;
//        protected readonly ITaxService _taxService;
//        protected readonly IUrlRecordService _urlRecordService;
//        protected readonly IVendorService _vendorService;
//        protected readonly IWebHelper _webHelper;
//        protected readonly IWorkContext _workContext;
//        protected readonly MediaSettings _mediaSettings;
//        protected readonly OrderSettings _orderSettings;
//        protected readonly RewardPointsSettings _rewardPointsSettings;
//        protected readonly ShippingSettings _shippingSettings;
//        protected readonly ShoppingCartSettings _shoppingCartSettings;
//        protected readonly TaxSettings _taxSettings;
//        protected readonly VendorSettings _vendorSettings;
//        private static readonly char[] _separator = [','];

//        #endregion

//        #region Ctor

//        public OverridenShoppingCartModelFactory(AddressSettings addressSettings,
//            CaptchaSettings captchaSettings,
//            CatalogSettings catalogSettings,
//            CommonSettings commonSettings,
//            CustomerSettings customerSettings,
//            IAddressModelFactory addressModelFactory,
//            IAddressService addressService,
//            IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeParser,
//            IAttributeService<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeService,
//            ICheckoutAttributeFormatter checkoutAttributeFormatter,
//            ICountryService countryService,
//            ICurrencyService currencyService,
//            ICustomerService customerService,
//            IDateTimeHelper dateTimeHelper,
//            IDiscountService discountService,
//            IDownloadService downloadService,
//            IGenericAttributeService genericAttributeService,
//            IGiftCardService giftCardService,
//            IHttpContextAccessor httpContextAccessor,
//            ILocalizationService localizationService,
//            IOrderProcessingService orderProcessingService,
//            IOrderTotalCalculationService orderTotalCalculationService,
//            IPaymentPluginManager paymentPluginManager,
//            IPaymentService paymentService,
//            IPermissionService permissionService,
//            IPictureService pictureService,
//            IPriceFormatter priceFormatter,
//            IProductAttributeFormatter productAttributeFormatter,
//            IProductService productService,
//            IShippingService shippingService,
//            IShoppingCartService shoppingCartService,
//            IShortTermCacheManager shortTermCacheManager,
//            IStateProvinceService stateProvinceService,
//            IStaticCacheManager staticCacheManager,
//            IStoreContext storeContext,
//            IStoreMappingService storeMappingService,
//            ITaxService taxService,
//            IUrlRecordService urlRecordService,
//            IVendorService vendorService,
//            IWebHelper webHelper,
//            IWorkContext workContext,
//            MediaSettings mediaSettings,
//            OrderSettings orderSettings,
//            RewardPointsSettings rewardPointsSettings,
//            ShippingSettings shippingSettings,
//            ShoppingCartSettings shoppingCartSettings,
//            TaxSettings taxSettings,
//            VendorSettings vendorSettings) : base(addressSettings,
//captchaSettings,
//catalogSettings,
//commonSettings,
//customerSettings,
//addressModelFactory,
//addressService,
//checkoutAttributeParser,
//checkoutAttributeService,
//checkoutAttributeFormatter,
//countryService,
//currencyService,
//customerService,
//dateTimeHelper,
//discountService,
//downloadService,
//genericAttributeService,
//giftCardService,
//httpContextAccessor,
//localizationService,
//orderProcessingService,
//orderTotalCalculationService,
//paymentPluginManager,
//paymentService,
//permissionService,
//pictureService,
//priceFormatter,
//productAttributeFormatter,
//productService,
//shippingService,
//shoppingCartService,
//shortTermCacheManager,
//stateProvinceService,
//staticCacheManager,
//storeContext,
//storeMappingService,
//taxService,
//urlRecordService,
//vendorService,
//webHelper,
//workContext,
//mediaSettings,
//orderSettings,
//rewardPointsSettings,
//shippingSettings,
//shoppingCartSettings,
//taxSettings,
//vendorSettings
//)
//        {
//            _addressSettings = addressSettings;
//            _addressService = addressService;
//            _captchaSettings = captchaSettings;
//            _catalogSettings = catalogSettings;
//            _commonSettings = commonSettings;
//            _customerSettings = customerSettings;
//            _addressModelFactory = addressModelFactory;
//            _checkoutAttributeParser = checkoutAttributeParser;
//            _checkoutAttributeService = checkoutAttributeService;
//            _checkoutAttributeFormatter = checkoutAttributeFormatter;
//            _countryService = countryService;
//            _currencyService = currencyService;
//            _customerService = customerService;
//            _dateTimeHelper = dateTimeHelper;
//            _discountService = discountService;
//            _downloadService = downloadService;
//            _genericAttributeService = genericAttributeService;
//            _giftCardService = giftCardService;
//            _httpContextAccessor = httpContextAccessor;
//            _localizationService = localizationService;
//            _orderProcessingService = orderProcessingService;
//            _orderTotalCalculationService = orderTotalCalculationService;
//            _paymentPluginManager = paymentPluginManager;
//            _paymentService = paymentService;
//            _permissionService = permissionService;
//            _pictureService = pictureService;
//            _priceFormatter = priceFormatter;
//            _productAttributeFormatter = productAttributeFormatter;
//            _productService = productService;
//            _shippingService = shippingService;
//            _shoppingCartService = shoppingCartService;
//            _shortTermCacheManager = shortTermCacheManager;
//            _stateProvinceService = stateProvinceService;
//            _staticCacheManager = staticCacheManager;
//            _storeContext = storeContext;
//            _storeMappingService = storeMappingService;
//            _taxService = taxService;
//            _urlRecordService = urlRecordService;
//            _vendorService = vendorService;
//            _webHelper = webHelper;
//            _workContext = workContext;
//            _mediaSettings = mediaSettings;
//            _orderSettings = orderSettings;
//            _rewardPointsSettings = rewardPointsSettings;
//            _shippingSettings = shippingSettings;
//            _shoppingCartSettings = shoppingCartSettings;
//            _taxSettings = taxSettings;
//            _vendorSettings = vendorSettings;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Prepare the order totals model
//        /// </summary>
//        /// <param name="cart">List of the shopping cart item</param>
//        /// <param name="isEditable">Whether model is editable</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the order totals model
//        /// </returns>
//        public override async Task<OrderTotalsModel> PrepareOrderTotalsModelAsync(IList<ShoppingCartItem> cart, bool isEditable)
//        {
//            var model = new OrderTotalsModel
//            {
//                IsEditable = isEditable
//            };

//            if (cart.Any())
//            {
//                //subtotal
//                var subTotalIncludingTax = await _workContext.GetTaxDisplayTypeAsync() == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal;
//                var (orderSubTotalDiscountAmountBase, _, subTotalWithoutDiscountBase, _, _) = await _orderTotalCalculationService.GetShoppingCartSubTotalAsync(cart, subTotalIncludingTax);
//                var subtotalBase = subTotalWithoutDiscountBase;
//                var currentCurrency = await _workContext.GetWorkingCurrencyAsync();
//                var subtotal = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(subtotalBase, currentCurrency);
//                var currentLanguage = await _workContext.GetWorkingLanguageAsync();
//                model.SubTotal = await _priceFormatter.FormatPriceAsync(subtotal, true, currentCurrency, currentLanguage.Id, subTotalIncludingTax);

//                if (orderSubTotalDiscountAmountBase > decimal.Zero)
//                {
//                    var orderSubTotalDiscountAmount = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(orderSubTotalDiscountAmountBase, currentCurrency);
//                    model.SubTotalDiscount = await _priceFormatter.FormatPriceAsync(-orderSubTotalDiscountAmount, true, currentCurrency, currentLanguage.Id, subTotalIncludingTax);
//                }

//                //shipping info
//                model.RequiresShipping = await _shoppingCartService.ShoppingCartRequiresShippingAsync(cart);
//                var customer = await _workContext.GetCurrentCustomerAsync();
//                var store = await _storeContext.GetCurrentStoreAsync();
//                if (model.RequiresShipping)
//                {
//                    var shoppingCartShippingBase = await _orderTotalCalculationService.GetShoppingCartShippingTotalAsync(cart);
//                    if (shoppingCartShippingBase.HasValue)
//                    {
//                        var shoppingCartShipping = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(shoppingCartShippingBase.Value, currentCurrency);
//                        model.Shipping = await _priceFormatter.FormatShippingPriceAsync(shoppingCartShipping, true);

//                        //selected shipping method
//                        var shippingOption = await _genericAttributeService.GetAttributeAsync<ShippingOption>(customer,
//                            NopCustomerDefaults.SelectedShippingOptionAttribute, store.Id);
//                        if (shippingOption != null)
//                            model.SelectedShippingMethod = shippingOption.Name;
//                    }
//                }
//                else
//                {
//                    model.HideShippingTotal = _shippingSettings.HideShippingTotal;
//                }

//                //payment method fee
//                var paymentMethodSystemName = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.SelectedPaymentMethodAttribute, store.Id);
//                var paymentMethodAdditionalFee = await _paymentService.GetAdditionalHandlingFeeAsync(cart, paymentMethodSystemName);
//                var (paymentMethodAdditionalFeeWithTaxBase, _) = await _taxService.GetPaymentMethodAdditionalFeeAsync(paymentMethodAdditionalFee, customer);
//                if (paymentMethodAdditionalFeeWithTaxBase > decimal.Zero)
//                {
//                    var paymentMethodAdditionalFeeWithTax = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(paymentMethodAdditionalFeeWithTaxBase, currentCurrency);
//                    model.PaymentMethodAdditionalFee = await _priceFormatter.FormatPaymentMethodAdditionalFeeAsync(paymentMethodAdditionalFeeWithTax, true);
//                }

//                //tax
//                bool displayTax;
//                bool displayTaxRates;
//                if (_taxSettings.HideTaxInOrderSummary && await _workContext.GetTaxDisplayTypeAsync() == TaxDisplayType.IncludingTax)
//                {
//                    displayTax = false;
//                    displayTaxRates = false;
//                }
//                else
//                {
//                    var (shoppingCartTaxBase, taxRates) = await _orderTotalCalculationService.GetTaxTotalAsync(cart);
//                    var shoppingCartTax = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(shoppingCartTaxBase, currentCurrency);

//                    if (shoppingCartTaxBase == 0 && _taxSettings.HideZeroTax)
//                    {
//                        displayTax = false;
//                        displayTaxRates = false;
//                    }
//                    else
//                    {
//                        displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
//                        displayTax = !displayTaxRates;

//                        model.Tax = await _priceFormatter.FormatPriceAsync(shoppingCartTax, true, false);
//                        foreach (var tr in taxRates)
//                        {
//                            model.TaxRates.Add(new OrderTotalsModel.TaxRate
//                            {
//                                Rate = _priceFormatter.FormatTaxRate(tr.Key),
//                                Value = await _priceFormatter.FormatPriceAsync(await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(tr.Value, currentCurrency), true, false),
//                            });
//                        }
//                    }
//                }

//                model.DisplayTaxRates = displayTaxRates;
//                model.DisplayTax = displayTax;

//                //total
//                var (shoppingCartTotalBase, orderTotalDiscountAmountBase, _, appliedGiftCards, redeemedRewardPoints, redeemedRewardPointsAmount) = await _orderTotalCalculationService.GetShoppingCartTotalAsync(cart);
//                if (shoppingCartTotalBase.HasValue)
//                {
//                    var shoppingCartTotal = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(shoppingCartTotalBase.Value, currentCurrency);
//                    model.OrderTotal = await _priceFormatter.FormatPriceAsync(shoppingCartTotal, true, false);
//                }

//                //discount
//                if (orderTotalDiscountAmountBase > decimal.Zero)
//                {
//                    var orderTotalDiscountAmount = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(orderTotalDiscountAmountBase, currentCurrency);
//                    model.OrderTotalDiscount = await _priceFormatter.FormatPriceAsync(-orderTotalDiscountAmount, true, false);
//                }

//                //gift cards
//                if (appliedGiftCards != null && appliedGiftCards.Any())
//                {
//                    foreach (var appliedGiftCard in appliedGiftCards)
//                    {
//                        var gcModel = new OrderTotalsModel.GiftCard
//                        {
//                            Id = appliedGiftCard.GiftCard.Id,
//                            CouponCode = appliedGiftCard.GiftCard.GiftCardCouponCode,
//                        };
//                        var amountCanBeUsed = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(appliedGiftCard.AmountCanBeUsed, currentCurrency);
//                        gcModel.Amount = await _priceFormatter.FormatPriceAsync(-amountCanBeUsed, true, false);

//                        var remainingAmountBase = await _giftCardService.GetGiftCardRemainingAmountAsync(appliedGiftCard.GiftCard) - appliedGiftCard.AmountCanBeUsed;
//                        var remainingAmount = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(remainingAmountBase, currentCurrency);
//                        gcModel.Remaining = await _priceFormatter.FormatPriceAsync(remainingAmount, true, false);

//                        model.GiftCards.Add(gcModel);
//                    }
//                }

//                //reward points to be spent (redeemed)
//                if (redeemedRewardPointsAmount > decimal.Zero)
//                {
//                    var redeemedRewardPointsAmountInCustomerCurrency = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(redeemedRewardPointsAmount, currentCurrency);
//                    model.RedeemedRewardPoints = redeemedRewardPoints;
//                    model.RedeemedRewardPointsAmount = await _priceFormatter.FormatPriceAsync(-redeemedRewardPointsAmountInCustomerCurrency, true, false);
//                }

//                //reward points to be earned
//                if (_rewardPointsSettings.Enabled && _rewardPointsSettings.DisplayHowMuchWillBeEarned && shoppingCartTotalBase.HasValue)
//                {
//                    //get shipping total
//                    var shippingBaseInclTax = !model.RequiresShipping ? 0 : (await _orderTotalCalculationService.GetShoppingCartShippingTotalAsync(cart, true)).shippingTotal ?? 0;

//                    //get total for reward points
//                    var totalForRewardPoints = _orderTotalCalculationService
//                        .CalculateApplicableOrderTotalForRewardPoints(shippingBaseInclTax, shoppingCartTotalBase.Value);
//                    if (totalForRewardPoints > decimal.Zero)
//                        model.WillEarnRewardPoints = await _orderTotalCalculationService.CalculateRewardPointsAsync(customer, totalForRewardPoints);
//                }
//            }

//            return model;
//        }

//        #endregion

//    }
//}
