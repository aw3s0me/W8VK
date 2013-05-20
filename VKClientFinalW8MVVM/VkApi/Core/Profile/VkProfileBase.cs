using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Core.Profile
{
    public class VkProfileBase
    {
        public long Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Uri Photo
        {
            get;
            set;
        }
        public Uri PhotoMedium
        {
            get;
            set;
        }
        public Uri PhotoBig
        {
            get;
            set;
        }
    }
}
