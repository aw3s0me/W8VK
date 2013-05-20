using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Error
{
    public class VkException : Exception
    {
        public string Error
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public VkException()
        {
        }
        public VkException(string error, string description)
        {
            this.Error = error;
            this.Description = description;
        }
    }
}
