using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.DiscountRules.CustomDiscounts.Helpers;
using Nop.Plugin.DiscountRules.CustomDiscounts.Services;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderApiController : BaseController
    {
        private readonly IOrderApiService _orderApiService;
        private readonly JwtAuthHelper _jwtAuthHelper;

        public OrderApiController(IOrderApiService orderApiService,
            JwtAuthHelper jwtAuthHelper)
        {
            _orderApiService = orderApiService;
            _jwtAuthHelper = jwtAuthHelper;
        }

        [HttpGet("order-details")]
        public async Task<IActionResult> GetOrdersByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var orders = await _orderApiService.SearchOrdersAsync(email: email);

            var result = orders.Select(o => new
            {
                orderId = o.Id,
                totalAmount = o.OrderTotal,
                orderDate = o.CreatedOnUtc
            });

            return Ok(result);
        }
    }
}
