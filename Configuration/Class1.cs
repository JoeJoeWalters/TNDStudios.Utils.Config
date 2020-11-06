using System;
using System.Collections.Generic;

namespace TNDStudios.Utils.Config
{
    public class TaxonomyContainer
    {
        public Dictionary<String, Taxonomy> Taxonomies { get; set; }
    }

    public class Taxonomy
    {
        public String Id { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; } = String.Empty;
        public Dictionary<String, TaxonomyNode> Nodes { get; set; }
    }

    public class TaxonomyNode
    {
        public Dictionary<String, TaxonomyProperty> Properties { get; set; }
    }

    public class TaxonomyProperty
    {
        public Type Type { get; set; } = typeof(String);
        public String Value { get; set; } = String.Empty;
    }
}
