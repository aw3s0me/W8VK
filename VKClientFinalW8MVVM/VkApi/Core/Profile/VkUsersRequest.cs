using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Profile
{
    public class VkUsersRequest
    {
        public async Task<IEnumerable<VkProfile>> Get(IEnumerable<string> uids, string fields, string nameCase)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (uids != null)
            {
                dictionary.Add("uids", string.Join(",", uids));
            }
            if (!string.IsNullOrWhiteSpace(fields))
            {
                dictionary.Add("fields", fields);
            }
            if (!string.IsNullOrWhiteSpace(nameCase))
            {
                dictionary.Add("name_case", nameCase);
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/users.get"), dictionary, "GET").Execute();
            VkErrorProcessor.ProcessError(jObject);
            IEnumerable<VkProfile> result;
            if (jObject["response"].HasValues)
            {
                result = Enumerable.Select<JToken, VkProfile>(jObject["response"], (JToken u) => VkProfile.FromJson(u));
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
