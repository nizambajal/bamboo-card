using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nop.Plugin.DiscountRules.CustomDiscounts.Domains;
using Nop.Plugin.DiscountRules.CustomDiscounts.Helpers;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : BaseController
    {
        private readonly JwtAuthHelper _jwtAuthHelper;

        public TokenController(JwtAuthHelper jwtAuthHelper)
        {
            _jwtAuthHelper = jwtAuthHelper;
        }

        [HttpPost("generate-token")]
        public IActionResult Generate([FromBody] JsonRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(request.Email);

            var token = _jwtAuthHelper.GenerateToken(request.Email);
            return Ok(new { token });
        }
    }
}
