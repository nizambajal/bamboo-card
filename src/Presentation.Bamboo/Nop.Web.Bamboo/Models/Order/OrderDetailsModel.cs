﻿using Nop.Web.Framework.Models;
using Nop.Web.Models.Common;
using Nop.Web.Models.Media;

namespace Nop.Web.Models.Order;

public partial record OrderDetailsModel
{
    public string GiftMessage { get; set; }
}