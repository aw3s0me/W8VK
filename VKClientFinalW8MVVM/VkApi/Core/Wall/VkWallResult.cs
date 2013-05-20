using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Core.Wall
{
    public class VkWallResult
    {
        public int Count
        {
            get;
            set;
        }
        public IEnumerable<VkWallEntry> Posts
        {
            get;
            set;
        }
    }
}
