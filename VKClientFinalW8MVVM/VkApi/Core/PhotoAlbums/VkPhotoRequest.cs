using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.PhotoAlbums
{
    public class VkPhotoRequest
    {
        public async Task<IEnumerable<VkPhoto>> Get(string oid, string aid, IList<string> pids = null, int rev = 0, int extended = 0, string feed_type = null, int feed = 0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (pids != null)
            {
                dictionary.Add("pids", string.Join(",", pids));
            }
            if (!string.IsNullOrEmpty(oid))
            {
                dictionary.Add("oid", oid);
            }
            if (!string.IsNullOrEmpty(aid))
            {
                dictionary.Add("aid", aid);
            }
            dictionary.Add("rev", rev.ToString());
            dictionary.Add("extended", extended.ToString());
            if (!string.IsNullOrEmpty(feed_type))
            {
                dictionary.Add("feed_type", feed_type);
            }
            if (feed!=0)
            {
                dictionary.Add("feed", feed.ToString());
            }

            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/photos.get"), dictionary, "GET").Execute();
            IEnumerable<VkPhoto> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    result = Enumerable.Select<JToken, VkPhoto>(Enumerable.Skip<JToken>(jObject["response"], 1), (JToken a) => VkPhoto.FromJson(a));
                    //result = Enumerable.Select<JToken, VkPhotoAlbum>(Enumerable.Where<JToken>(jObject["response"], (JToken v) => v.HasValues), (JToken v) => VkPhotoAlbum.FromJson(v));
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
