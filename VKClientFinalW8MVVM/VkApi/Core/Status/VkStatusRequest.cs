using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.Status
{
    public class VkStatusRequest
    {
        public async Task<bool> Set(string text, string audioId, string audioOwnerId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(text))
            {
                dictionary.Add("text", text);
            }
            if (!string.IsNullOrEmpty(audioId) && !string.IsNullOrEmpty(audioOwnerId))
            {
                dictionary.Add("audio", audioOwnerId + "_" + audioId);
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/status.set"), dictionary, "GET").Execute();
            bool result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = false;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = (jObject["response"].Value<int>() == 1);
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
