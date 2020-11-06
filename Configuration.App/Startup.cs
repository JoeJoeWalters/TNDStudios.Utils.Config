using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Utils.Config;

[assembly: FunctionsStartup(typeof(Configuration.App.Startup))]

namespace Configuration.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<TaxonomyContainer>(
                new TaxonomyContainer()
                {
                    Taxonomies = new Dictionary<string, Taxonomy>()
                    {
                        {
                            "main",
                            new Taxonomy()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "main",
                                Nodes = new Dictionary<string, TaxonomyNode>()
                                {
                                    { 
                                        "Tenant1.Application1", 
                                        new TaxonomyNode()
                                        {
                                            Properties = new Dictionary<string, TaxonomyProperty>()
                                            {
                                                {
                                                    "API::Unchanging",
                                                    new TaxonomyProperty()
                                                    {
                                                        Type = typeof(String),
                                                        Value = "Some Random Value"
                                                    }
                                                },
                                                {
                                                    "API::Version", 
                                                    new TaxonomyProperty() 
                                                    { 
                                                        Type = typeof(float),
                                                        Value = "3.1"
                                                    } 
                                                }
                                            }
                                        } 
                                    },
                                    {
                                        "Tenant1.Application1.Module1",
                                        new TaxonomyNode()
                                        {
                                            Properties = new Dictionary<string, TaxonomyProperty>()
                                            {
                                                {
                                                    "API::Version",
                                                    new TaxonomyProperty()
                                                    {
                                                        Type = typeof(float),
                                                        Value = "3.2"
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    {
                                        "Tenant1.Application1.Module2",
                                        new TaxonomyNode()
                                        {
                                            Properties = new Dictionary<string, TaxonomyProperty>()
                                            {
                                                {
                                                    "API::Version",
                                                    new TaxonomyProperty()
                                                    {
                                                        Type = typeof(float),
                                                        Value = "3.3"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                });

            /*
            builder.Services.AddSingleton<IMyService>((s) => {
                return new MyService();
            });

            builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
            */
        }
    }
}
