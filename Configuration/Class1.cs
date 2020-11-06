using System;
using System.Collections.Generic;

namespace TNDStudios.Utils.Config
{
    public class Taxonomy
    {
        public String Id { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; } = String.Empty;
        Dictionary<String, TaxonomyNode> Nodes { get; set; }
    }

    public class TaxonomyNode
    {
        public String Id { get; set; } = Guid.NewGuid().ToString();
        public String Path { get; set; } = String.Empty;
        Dictionary<String, TaxonomyProperty> Properties { get; set; }
    }

    public class TaxonomyProperty
    {
        public String Id { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; } = String.Empty;
        public String Value { get; set; } = String.Empty;
    }
}
