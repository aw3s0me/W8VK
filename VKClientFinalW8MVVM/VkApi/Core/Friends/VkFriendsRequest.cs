using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Profile;

namespace VkApi.Core.Friends
{
    public class VkFriendsRequest
    {
        public async Task<IEnumerable<VkProfile>> Get(string uid, string fields, string nameCase, int count, int offset, bool sortByRating = true)
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
            if (!string.IsNullOrWhiteSpace(nameCase))
            {
                dictionary.Add("name_case", nameCase);
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString());
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString());
            }
            if (sortByRating)
            {
                dictionary.Add("order", "hints");
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/friends.get"), dictionary, "GET").Execute();
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
