using MediatR;
using Microsoft.Extensions.DependencyInjection;
using stock_api_application.Interfaces;
using stock_api_application.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace stock_api_application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IValidateIncomingItems, ValidateIncomingItems>();
        }
    }
}
