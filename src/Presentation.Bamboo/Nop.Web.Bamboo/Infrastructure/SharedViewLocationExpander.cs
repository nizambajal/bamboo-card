﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Web.Infrastructure
{
    public class SharedViewLocationExpander : IViewLocationExpander
    {
        /// <summary>
        /// Invoked by a <see cref="T:Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" /> to determine the values that would be consumed by this instance
        /// of <see cref="T:Microsoft.AspNetCore.Mvc.Razor.IViewLocationExpander" />. The calculated values are used to determine if the view location
        /// has changed since the last time it was located.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Razor.ViewLocationExpanderContext" /> for the current view location
        /// expansion operation.</param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        /// <summary>
        /// Invoked by a <see cref="T:Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" /> to determine potential locations for a view.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Razor.ViewLocationExpanderContext" /> for the current view location
        /// expansion operation.</param>
        /// <param name="viewLocations">The sequence of view locations to expand.</param>
        /// <returns>A list of expanded view locations.</returns>        
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Areas/Admin/Views/Overriden/{{1}}/{{0}}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { 
                    $"/Views/Overriden/{{1}}/{{0}}.cshtml",
                    $"/Views/Overriden/Shared/{{0}}.cshtml"
                }.Concat(viewLocations);
            }

            return viewLocations;
        } 
    }
}
