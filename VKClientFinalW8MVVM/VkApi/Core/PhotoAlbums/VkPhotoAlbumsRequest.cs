using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VkApi.Core.PhotoAlbums
{
    public class VkPhotoAlbumsRequest
    {
        public async Task<IEnumerable<VkPhotoAlbum>> Get(string uid, IList<string> aids = null, int needSystem = 0, int needCovers = 0, int needSizes = 0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (aids != null)
            {
                dictionary.Add("aids", string.Join(",", aids));
            }
            if (!string.IsNullOrEmpty(uid))
            {
                dictionary.Add("uid", uid);
            }
            dictionary.Add("need_system", needSystem.ToString());
            dictionary.Add("need_covers" , needCovers.ToString());
            dictionary.Add("photo_sizes", needSizes.ToString());

            JObject jObject = await new VkRequest(new Uri("https://api.vk.com/method/photos.getAlbums"), dictionary, "GET").Execute();
            IEnumerable<VkPhotoAlbum> result;
            if (VkErrorProcessor.ProcessError(jObject))
            {
                result = null;
            }
            else
            {
                if (jObject["response"].HasValues)
                {
                    //result = Enumerable.Select<JToken, VkPhotoAlbum>(Enumerable.Skip<JToken>(jObject["response"], 1), (JToken a) => VkPhotoAlbum.FromJson(a));
                    result = Enumerable.Select<JToken, VkPhotoAlbum>(Enumerable.Where<JToken>(jObject["response"], (JToken v) => v.HasValues), (JToken v) => VkPhotoAlbum.FromJson(v));
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
