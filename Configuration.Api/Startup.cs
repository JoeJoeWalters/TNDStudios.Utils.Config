using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TNDStudios.Utils.Configuration;
using TNDStudios.Utils.Configuration.Mocks;

[assembly: FunctionsStartup(typeof(Configuration.Api.Startup))]

namespace Configuration.Api
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
