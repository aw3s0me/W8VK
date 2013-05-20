using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkApi.Core.Audio;

namespace VkApi.Extensions
{
    public static class AudioExtensions
    {
        public static Task<IEnumerable<VkAudio>> Get(this VkAudioAlbum album)
        {
            return Vkontakte.Instance.Audio.Get(album.OwnerId.ToString(CultureInfo.InvariantCulture), null, album.Id.ToString(CultureInfo.InvariantCulture), false, 0, 0);
        }
    }
}
