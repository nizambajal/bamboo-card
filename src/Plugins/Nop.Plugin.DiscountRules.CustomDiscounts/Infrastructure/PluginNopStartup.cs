using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nop.Core.Infrastructure;
using Nop.Plugin.DiscountRules.CustomDiscounts.Controllers;
using Nop.Plugin.DiscountRules.CustomDiscounts.Helpers;
using Nop.Plugin.DiscountRules.CustomDiscounts.Services;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Infrastructure;

public class PluginNopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ViewLocationExpander());
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "bamboo-card",
                ValidAudience = "postman",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BambooCardSuperSecureSecretKey!@#123456"))
            };
        });

        //register services and interfaces
        services.AddScoped<IOrderTotalCalculationService, OverridenOrderTotalCalculationService>();
        services.AddScoped<IOrderApiService, OrderApiService>();
        services.AddScoped<JwtAuthHelper>();
        services.AddScoped<OrderApiController>();
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => int.MaxValue;
}