using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ExtensionLibrary.HttpRequests
{
    public static class HttpRequestsExtension
    {
        /// <summary>
        /// Sends a Http Get request to a URL.
        /// </summary>
        /// <typeparam name="T">The returned type from the call.</typeparam>
        /// <param name="pathAndQuery">The path and query to call.</param>
        /// <param name="mediaType">The media type to use i.e. application/xml or application/json.</param>
        /// <returns>An object specified in the generic type.</returns>
        public static T HttpGet<T>(string path, string baseAddress, string mediaType) where T : class
        {
            T result = default(T);

            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

                var response = client.GetAsync(path).Result;

                if (response.IsSuccessStatusCode)
                    result = response.Content.ReadAsAsync<T>().Result;
                else
                    return null;
            }

            return result;
        }
    }
}
