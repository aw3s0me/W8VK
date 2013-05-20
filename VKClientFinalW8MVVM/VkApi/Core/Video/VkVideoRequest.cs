using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VkApi.Core.Audio;

namespace VkApi.Core.Video
{
    public class VkVideoRequest
    {
        private const int MAX_VIDEO_COUNT = 200;
        public async Task<IEnumerable<VkVideo>> Get(IList<string> videos=null, string uid = null, string gid = null, string aid = null, int previewWidth = 0, int count = 0, int offset = 0)
        {
            if (count > 200)
            {
                throw new ArgumentException("Maximum count is 200.");
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (videos != null)
            {
                dictionary.Add("videos", string.Join(",", videos));
            }
            if (!string.IsNullOrEmpty(uid))
            {
                dictionary.Add("uid", uid);
            }
            if (!string.IsNullOrEmpty(gid))
            {
                dictionary.Add("gid", gid);
            }
            if (!string.IsNullOrEmpty(aid))
            {
                dictionary.Add("aid", aid);
            }
            if (previewWidth > 0)
            {
                dictionary.Add("width", previewWidth.ToString(CultureInfo.InvariantCulture));
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                dictionary.Add("count", 200.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/video.get"), dictionary, "GET").Execute();
            IEnumerable<VkVideo> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkVideo>(Enumerable.Where<JToken>(jObject["response"], (JToken v) => v.HasValues), (JToken v) => VkVideo.FromJson(v));
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public async Task<IEnumerable<VkVideo>> Search(string query, int count = 0, int offset = 0, bool hdOnly = false, VkAudioSortType sort = VkAudioSortType.DateAdded, bool adult = false)
        {
            if (count > 200)
            {
                throw new ArgumentException("Maximum count is 200.");
            }
            if (query == null)
            {
                throw new ArgumentException("Query must not be null.");
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("q", query);
            if (hdOnly)
            {
                dictionary.Add("hd", "1");
            }
            Dictionary<string, string> arg_98_0 = dictionary;
            string arg_98_1 = "sort";
            int num = (int)sort;
            arg_98_0.Add(arg_98_1, num.ToString(CultureInfo.InvariantCulture));
            if (adult)
            {
                dictionary.Add("adult", "1");
            }
            if (count > 0)
            {
                dictionary.Add("count", count.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                dictionary.Add("count", 200.ToString(CultureInfo.InvariantCulture));
            }
            if (offset > 0)
            {
                dictionary.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            }
            dictionary.Add("access_token", Vkontakte.Instance.AccessToken.Token);
            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/video.search"), dictionary, "GET").Execute();
            IEnumerable<VkVideo> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkVideo>(jObject["response"], (JToken v) => VkVideo.FromJson(v));
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
