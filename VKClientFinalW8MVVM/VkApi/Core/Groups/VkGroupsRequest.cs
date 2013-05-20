using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Groups
{
    public class VkGroupsRequest
    {
        public async Task<IEnumerable<VkGroup>> Get(string uid, string fields, string filter, int count, int offset, bool extended = true)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(uid))
            {
                dictionary.Add("uid", uid);
            }
            if (!string.IsNullOrWhiteSpace(fields))
            {
                dictionary.Add("fields", fields);
            }
            if (!string.IsNullOrWhiteSpace(filter))
            {
                dictionary.Add("filter", filter);
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString());
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString());
            }
            if (extended)
            {
                dictionary.Add("extended", "1");
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/groups.get"), dictionary, "GET").Execute();
            VkErrorProcessor.ProcessError(jObject);
            IEnumerable<VkGroup> result;
            if (jObject["response"].HasValues)
            {
                result = Enumerable.Select<JToken, VkGroup>(Enumerable.Where<JToken>(jObject["response"], (JToken g) => g.HasValues), (JToken g) => VkGroup.FromJson(g));
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
