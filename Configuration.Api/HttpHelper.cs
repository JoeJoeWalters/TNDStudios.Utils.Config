using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configuration.Api
{
    public static class HttpHelper
    {
        public static T GetQueryValue<T>(this HttpRequest request, string key, T defaultValue)
        {
            Type type = typeof(T);
            T result = defaultValue;
            request.Query.TryGetValue(key, out StringValues values);
            if (values != default(StringValues) && values.Count != 0)
            {
                if (values[0] != String.Empty)
                    result = (T)Convert.ChangeType(values[0], type);
            }
            return result;
        }
    }
}
