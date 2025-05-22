using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.DiscountRules.CustomDiscounts
{
    public class DiscountSettings : ISettings
    {
        /// <summary>
        /// Gets or sets enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets discount percentage
        /// </summary>
        public double DiscountPercentage { get; set; }
    }
}
