using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApi.Extensions
{
    internal static class StreamExtensions
    {
        public static int CopyStream(this Stream source, Stream dest)
        {
            long position = source.Position;
            long position2 = dest.Position;
            byte[] array = new byte[4096];
            int num = 0;
            int num2;
            while ((num2 = source.Read(array, 0, array.Length)) > 0)
            {
                num += num2;
                dest.Write(array, 0, num2);
            }
            if (source.CanSeek)
            {
                source.Seek(position, 0);
            }
            if (dest.CanSeek)
            {
                dest.Seek(position2, 0);
            }
            return num;
        }
    }
}
