using System;

namespace VKModel.Exceptions
{
    public class VkException : Exception
    {
        public  VkException() {}

        public  VkException(string msg):base(msg) {}

        public  VkException(string msg, Exception innerException):base (msg, innerException) {}

    }
}
