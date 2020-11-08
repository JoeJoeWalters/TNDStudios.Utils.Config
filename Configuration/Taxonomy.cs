using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TNDStudios.Utils.Configuration
{
    public abstract class TaxonomyContainer
    {
        public Dictionary<String, Taxonomy> Taxonomies { get; set; }

        private string GetParent(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                List<string> _nodes = path.Split('.').ToList();
                _nodes.RemoveAt(_nodes.Count - 1);
                if (_nodes.Count != 0)
                {
                    return String.Join(".", _nodes);
                }
            }

            return String.Empty;
        }

        public Boolean Write(string taxonomy, string path, KeyValuePair<String, TaxonomyProperty> property)
        {
            Taxonomies.TryGetValue(taxonomy, out Taxonomy _taxonomy);
            if (_taxonomy == null)
            {
                _taxonomy = new Taxonomy();
                Taxonomies.Add(taxonomy, _taxonomy);
            }

            _taxonomy.Nodes.TryGetValue(path, out TaxonomyNode _node);
            if (_node == null)
            {
                _node = new TaxonomyNode();
                _taxonomy.Nodes.Add(path, _node);
            }

            _node.Properties[property.Key] = property.Value;

            return true;
        }

        public Dictionary<String, TaxonomyProperty> Read(string taxonomy, string path)
        {
            Dictionary<String, TaxonomyProperty> _result = new Dictionary<string, TaxonomyProperty>();

            Taxonomies.TryGetValue(taxonomy, out Taxonomy _taxonomy);
            if (_taxonomy != null)
            {
                _taxonomy.Nodes.TryGetValue(path, out TaxonomyNode _node);
                if (_node != null)
                {
                    foreach (string key in _node.Properties.Keys)
                    {
                        _result[key] = _node.Properties[key];
                    }
                }

                // Regardless of if a node was found, move to the parent
                // Get the parent properties and add them in where they don't exist
                string _parentPath = GetParent(path);
                if (_parentPath != String.Empty)
                {
                    Dictionary<string, TaxonomyProperty> _parent = Read(taxonomy, _parentPath);
                    foreach (string key in _parent.Keys)
                    {
                        if (!_result.ContainsKey(key))
                            _result[key] = _parent[key];
                    }
                }
            }

            return _result;
        }
    }

    public class Taxonomy
    {
        public Dictionary<String, TaxonomyNode> Nodes { get; set; }
    }

    public class TaxonomyNode
    {
        public Dictionary<String, TaxonomyProperty> Properties { get; set; }
    }

    public class TaxonomyProperty
    {
        [JsonConverter(typeof(DataTypeConverter))]
        public Type Type { get; set; } = typeof(String);
        public String Value { get; set; } = String.Empty;
    }

    public class DataTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Type value = Type.GetType(reader.Value.ToString());
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}