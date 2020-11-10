using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StrategyGame.API.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StrategyGame.API.Infrastructure.Middlewares
{
    public class SpaConfigMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SpaConfiguration spaConfiguration;

        public SpaConfigMiddleware(
            RequestDelegate next,
            IOptions<SpaConfiguration> options)
        {
            _next = next;
            spaConfiguration = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/assets/config.json")
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(spaConfiguration));
                return;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
