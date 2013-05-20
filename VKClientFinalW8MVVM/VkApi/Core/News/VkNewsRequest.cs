using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Audio;

namespace VkApi.Core.News
{
    public class VkNewsRequest
    {
        public VkNewsRequest()
        {
        }

        [DebuggerStepThrough]
        public async Task<List<VkNewsEntry>> Get(string sourceIds = null, string filters = null, int count = 0, int offset = 0)
        {
             Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(sourceIds))
            {
                dictionary.Add("source_ids", sourceIds);
            }
            //filters определяет какие типы сообщений на стене надо получить
            if (!string.IsNullOrEmpty(filters))
            {
                dictionary.Add("filters", filters);
            }
            
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/newsfeed.get"), dictionary, "GET").Execute();
            List<VkNewsEntry> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    
                    result = new List<VkNewsEntry>();
                    //result = List.Select<JToken, VkNewsEntry>(jObject["response"], (JToken a) => VkNewsEntry.FromJson(a));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
    }
}
