using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Profile
{
    public class VkCountryRequest
    {
        public async Task<IEnumerable<VkCountry>> Get(IEnumerable<string> cids)
        {
            var dictionary = new Dictionary<string, string>();
            if (cids != null)
            {
                dictionary.Add("cids", string.Join(",", cids));
            }
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/places.getCountryById"), dictionary, "GET").Execute();
            VkErrorProcessor.ProcessError(jObject);
            IEnumerable<VkCountry> result;
            if (jObject["response"].HasValues)
            {
                result = Enumerable.Select<JToken, VkCountry>(jObject["response"], (JToken u) => VkCountry.FromJson(u));
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
