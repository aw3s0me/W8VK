using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class AudioChangedMessage
    {
        public Audio OldAudio
        {
            get;
            set;
        }
        public Audio NewAudio
        {
            get;
            set;
        }
    }
}
