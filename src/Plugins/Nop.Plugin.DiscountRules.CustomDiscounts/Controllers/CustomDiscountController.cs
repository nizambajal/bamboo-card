using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Discounts;
using Nop.Plugin.DiscountRules.CustomDiscounts.Models;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    [AutoValidateAntiforgeryToken]

    public class CustomDiscountController : BasePluginController
    {

        #region Fields

        protected readonly ISettingService _settingService;
        protected readonly INotificationService _notificationService;
        protected readonly ILocalizationService _localizationService;
        protected readonly IStoreContext _storeContext;
        protected readonly IDiscountService _discountService;

        #endregion

        #region Ctor

        public CustomDiscountController(ISettingService settingService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            IDiscountService discountService)
        {
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _storeContext = storeContext;
            _discountService = discountService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get errors message from model state
        /// </summary>
        /// <param name="modelState">Model state</param>
        /// <returns>Errors message</returns>
        protected IEnumerable<string> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            return ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        }

        #endregion

        #region Methods

        [CheckPermission(StandardPermission.Promotions.DISCOUNTS_VIEW)]
        public async Task<IActionResult> Configure()
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var discountSettings = await _settingService.LoadSettingAsync<DiscountSettings>(storeId);

            //load the discount
            var model = new ConfigurationModel
            {
                Enabled = discountSettings.Enabled,
                DiscountPercentage = discountSettings.DiscountPercentage
            };

            model.ActiveStoreScopeConfiguration = storeId;

            if (storeId > 0)
                model.DiscountPercentage_OverrideForStore = await _settingService.SettingExistsAsync(discountSettings, settings => settings.DiscountPercentage, storeId);

            return View("~/Plugins/DiscountRules.CustomDiscounts/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [CheckPermission(StandardPermission.Promotions.DISCOUNTS_CREATE_EDIT_DELETE)]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
                var discountSettings = await _settingService.LoadSettingAsync<DiscountSettings>(storeId);

                discountSettings.Enabled = model.Enabled;
                discountSettings.DiscountPercentage = model.DiscountPercentage;

                await _settingService.SaveSettingOverridablePerStoreAsync(discountSettings, x=> x.Enabled, model.Enabled_OverrideForStore, storeId, false);
                await _settingService.SaveSettingOverridablePerStoreAsync(discountSettings, x=> x.DiscountPercentage, model.DiscountPercentage_OverrideForStore, storeId, false);
                await _settingService.ClearCacheAsync();

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            }

            return await Configure();
        }

        #endregion

    }
}
