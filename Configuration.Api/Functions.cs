using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TNDStudios.Utils.Configuration;
using System.Collections.Generic;

namespace Configuration.App
{
    public class Functions
    {
        private TaxonomyContainer _taxonomyContainer;

        public Functions(TaxonomyContainer taxonomyContainer)
        {
            _taxonomyContainer = taxonomyContainer;
        }

        [FunctionName("getproperties")]
        public async Task<IActionResult> GetProperties(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "properties/{taxonomy}/{path}")] HttpRequest req, string path, String taxonomy,
            ILogger log)
        {
            Dictionary<String, TaxonomyProperty> properties = _taxonomyContainer.Read(taxonomy, path);
            return new OkObjectResult(properties);
        }

        [FunctionName("postproperties")]
        public async Task<IActionResult> PostProperties(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "properties/{taxonomy}/{path}")] HttpRequest req, string path, String taxonomy,
            ILogger log)
        {
            Dictionary<String, TaxonomyProperty> values = JsonConvert.DeserializeObject<Dictionary<String, TaxonomyProperty>>(await req.ReadAsStringAsync());
            if (values == null)
                return new BadRequestResult();

            foreach (String key in values.Keys)
                _taxonomyContainer.Write(taxonomy, path, new KeyValuePair<String, TaxonomyProperty>(key, values[key]));

            Dictionary<String, TaxonomyProperty> properties = _taxonomyContainer.Read(taxonomy, path);
            return new OkObjectResult(properties);
        }

        private int List<T>()
        {
            throw new NotImplementedException();
        }
    }
}
