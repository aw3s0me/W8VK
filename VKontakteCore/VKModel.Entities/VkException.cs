using System;

namespace VKModel
{
    namespace Exceptions
    {
        public class VkException : Exception
        {
            public VkException() { }

            public VkException(string msg) : base(msg) { }

            public VkException(string msg, Exception innerException) : base(msg, innerException) { }

        }
    }
}



