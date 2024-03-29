﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3
{
    public class YourCustomMiddleMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public YourCustomMiddleMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {

            // Your HttpContext related task is in here.

            await _requestDelegate(context);
        }
    }
}
