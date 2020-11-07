using System;
using System.Collections.Generic;
using System.Text;
using TNDStudios.Utils.Configuration;

namespace TNDStudios.Utils.Configuration.Mocks
{
    public class TenantTaxonomyContainer : TaxonomyContainer
    {
        public TenantTaxonomyContainer()
        {
            Taxonomies =
                new Dictionary<string, Taxonomy>()
                    {
                        {
                            "main",
                            new Taxonomy()
                            {
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
                };
        }
    }
}
