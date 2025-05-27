using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.DiscountRules.CustomDiscounts;

/// <summary>
/// Rename this file and change to the correct type
/// </summary>
public class CustomDiscounts : BasePlugin, IMiscPlugin, IDiscountRequirementRule
{

    #region Fields

    protected readonly ISettingService _settingService;
    protected readonly ICustomerService _customerService;
    protected readonly IUrlHelperFactory _urlHelperFactory;
    protected readonly IActionContextAccessor _actionContextAccessor;
    protected readonly IWebHelper _webHelper;
    protected readonly ILocalizationService _localizationService;
    protected readonly IDiscountService _discountService;

    #endregion

    #region Ctor

    public CustomDiscounts(ISettingService settingService,
        ICustomerService customerService,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IWebHelper webHelper,
        ILocalizationService localizationService,
        IDiscountService discountService)
    {
        _settingService = settingService;
        _customerService = customerService;
        _urlHelperFactory = urlHelperFactory;
        _actionContextAccessor = actionContextAccessor;
        _webHelper = webHelper;
        _localizationService = localizationService;
        _discountService = discountService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Check discount requirement
    /// </summary>
    /// <param name="request">Object that contains all information required to check the requirement (Current customer, discount, etc)</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the result
    /// </returns>
    public async Task<DiscountRequirementValidationResult> CheckRequirementAsync(DiscountRequirementValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        //invalid by default
        var result = new DiscountRequirementValidationResult();

        if (request.Customer == null)
            return result;

        //try to get saved restricted customer role identifier
        var restrictedRoleId = await _settingService.GetSettingByKeyAsync<int>(string.Format(DiscountRequirementDefaults.SettingsKey, request.DiscountRequirementId));
        if (restrictedRoleId == 0)
            return result;

        //result is valid if the customer belongs to the restricted role
        result.IsValid = (await _customerService.GetCustomerRolesAsync(request.Customer)).Any(role => role.Id == restrictedRoleId);

        return result;
    }

    /// <summary>
    /// Get URL for rule configuration
    /// </summary>
    /// <param name="discountId">Discount identifier</param>
    /// <param name="discountRequirementId">Discount requirement identifier (if editing)</param>
    /// <returns>URL</returns>
    public string GetConfigurationUrl(int discountId, int? discountRequirementId)
    {
        return "";
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/CustomDiscount/Configure";
    }

    /// <summary>
    /// Install the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.DiscountRules.CustomDiscounts.Fields.Enabled"] = "Enabled",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.Enabled.Hint"] = "Enable/Disable the discounts",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.DiscountPercentage"] = "Discount Percentage",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.DiscountPercentage.Hint"] = "Discount percentage",
            ["Admin.Catalog.ProductAttributes.List.SearchProductAttributeName"] = "Name",
            ["Admin.Catalog.ProductAttributes.List.SearchProductAttributeName.Hint"] = "Search by Product Attribute Name",
            ["ShoppingCart.Giftmessage.Label"] = "Enter gift message",
            ["ShoppingCart.Giftmessage.Button"] = "Add gift message",
            ["ShoppingCart.Giftmessage"] = "Gift Message",
            ["ShoppingCart.Giftmessage.Tooltip"] = "Enter gift message",
            ["ShoppingCart.Giftmessage.Saved"] = "The gift message was saved",
            ["Order.GiftMessage"] = "Gift Message",
        });

        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstall the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task UninstallAsync()
    {
        //discount requirements
        var discountRequirements = (await _discountService.GetAllDiscountRequirementsAsync())
            .Where(discountRequirement => discountRequirement.DiscountRequirementRuleSystemName == DiscountRequirementDefaults.SystemName);
        foreach (var discountRequirement in discountRequirements)
        {
            await _discountService.DeleteDiscountRequirementAsync(discountRequirement, false);
        }

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.DiscountRules.CustomDiscounts");
        await _localizationService.DeleteLocaleResourcesAsync("Admin.Catalog.ProductAttributes.List.SearchProductAttributeName");
        await _localizationService.DeleteLocaleResourcesAsync("ShoppingCart.Giftmessage.Label");
        await _localizationService.DeleteLocaleResourcesAsync("ShoppingCart.Giftmessage.Button");
        await _localizationService.DeleteLocaleResourcesAsync("ShoppingCart.Giftmessage");
        await _localizationService.DeleteLocaleResourcesAsync("ShoppingCart.Giftmessage.Tooltip");
        await _localizationService.DeleteLocaleResourcesAsync("ShoppingCart.Giftmessage.Saved");
        await _localizationService.DeleteLocaleResourcesAsync("Order.GiftMessage");

        await base.UninstallAsync();
    }

    #endregion
}