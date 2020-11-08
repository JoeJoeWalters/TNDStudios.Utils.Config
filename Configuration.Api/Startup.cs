using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Utils.Configuration;
using TNDStudios.Utils.Configuration.Mocks;

[assembly: FunctionsStartup(typeof(Configuration.App.Startup))]

namespace Configuration.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Populate the Taxonomy Container with one of the mocks
            builder.Services.AddSingleton<TaxonomyContainer>(new TenantTaxonomyContainer());
        }
    }
}
