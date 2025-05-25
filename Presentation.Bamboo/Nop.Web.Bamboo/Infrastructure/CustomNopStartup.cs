using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Core.Infrastructure;
using Nop.Services.Orders;
using Nop.Web.Areas.Admin.Factories;

namespace Nop.Web.Infrastructure;

/// <summary>
/// Represents the registering services on application startup
/// </summary>
public partial class CustomNopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new SharedViewLocationExpander());
        });

        services.AddScoped<IProductAttributeModelFactory, OverridenProductAttributeModelFactory>();
        services.AddScoped<IOrderModelFactory, OverridenOrderModelFactory>();
        services.AddScoped<Web.Factories.IShoppingCartModelFactory, Web.Factories.OverridenShoppingCartModelFactory>();
        services.AddScoped<Web.Factories.IOrderModelFactory, Web.Factories.OverridenOrderModelFactory>();
        services.AddScoped<IOrderProcessingService, OverridenOrderProcessingService>();
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
    public int Order => 2003;
}
