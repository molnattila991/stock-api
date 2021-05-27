using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using stock_api_application.Interfaces;
using stock_api_infrastructure_in_memory.Repositories;

namespace stock_api_infrastructure_in_memory
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureInMemory(this IServiceCollection services)
        {
            services.AddSingleton<IStockRepository, StockRepository>();
            services.AddSingleton<IValidItemRepository, ValidItemRepository>();
        }
    }
}
