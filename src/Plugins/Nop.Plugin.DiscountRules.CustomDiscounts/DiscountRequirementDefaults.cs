using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.DiscountRules.CustomDiscounts;

/// <summary>
/// Represents defaults for the discount requirement rule
/// </summary>
public static class DiscountRequirementDefaults
{
    /// <summary>
    /// The system name of the discount requirement rule
    /// </summary>
    public static string SystemName => "DiscountRequirement.MustBeAssignedToCustomerRole";

    /// <summary>
    /// The key of the settings to save restricted customer roles
    /// </summary>
    public static string SettingsKey => "DiscountRequirement.MustBeAssignedToCustomerRole-{0}";

    /// <summary>
    /// The HTML field prefix for discount requirements
    /// </summary>
    public static string HtmlFieldPrefix => "DiscountRulesCustomerRoles{0}";
}
