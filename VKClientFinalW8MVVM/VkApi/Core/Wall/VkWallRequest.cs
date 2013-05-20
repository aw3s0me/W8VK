using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Wall
{
    public class VkWallRequest
    {
        public async Task<VkWallResult> Get(string ownerId, string filter, int count = 0, int offset = 0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(ownerId))
            {
                dictionary.Add("owner_id", ownerId);
            }
            if (!string.IsNullOrEmpty(filter))
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
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/wall.get"), dictionary, "GET").Execute();
            VkErrorProcessor.ProcessError(jObject);
            VkWallResult result;
            if (jObject["response"] != null)
            {
                VkWallResult vkWallResult = new VkWallResult();
                vkWallResult.Count = jObject["response"].First.Value<int>();
                vkWallResult.Posts = Enumerable.Select<JToken, VkWallEntry>(Enumerable.Where<JToken>(jObject["response"], (JToken p) => p.HasValues), (JToken p) => VkWallEntry.FromJson(p));
                result = vkWallResult;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
