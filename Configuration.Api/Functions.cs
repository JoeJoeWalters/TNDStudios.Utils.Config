using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TNDStudios.Utils.Configuration;

namespace Configuration.Api
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
            bool inherit = req.GetQueryValue<bool>("inherit", false);
            string renderType = req.GetQueryValue<string>("render", "system");
            Dictionary<String, TaxonomyProperty> properties = _taxonomyContainer.Read(taxonomy, path, inherit);
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

            Dictionary<String, TaxonomyProperty> properties = _taxonomyContainer.Read(taxonomy, path, false);
            return new OkObjectResult(properties);
        }

        [FunctionName("deleteproperties")]
        public async Task<IActionResult> DeleteProperties(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "properties/{taxonomy}/{path}/{propertyName}")] HttpRequest req, string path, String taxonomy, String propertyName,
            ILogger log)
        {
            Boolean result = _taxonomyContainer.Delete(taxonomy, path, propertyName);
            if (result)
                return new OkResult();
            else
                return new NotFoundResult();
        }
    }
}