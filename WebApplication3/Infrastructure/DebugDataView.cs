using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Infrastructure
{
    public class DebugDataView : IView
    {
        public string Path => String.Empty;

        public async Task RenderAsync(ViewContext context)
        {
            context.HttpContext.Response.ContentType = "text/plain";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("-------Route Data-------");
            foreach (var kvp in context.RouteData.Values)
            {
                sb.AppendLine($"Key: {kvp.Key}, Values: {kvp.Value}");
            }
            sb.AppendLine("----------View Data--------");
            foreach (var kvp in context.ViewData)
            {
                sb.AppendLine($"Key: {kvp.Key}, Values: {kvp.Value}");
            }

            await context.Writer.WriteAsync(sb.ToString());
        }
    }
}
