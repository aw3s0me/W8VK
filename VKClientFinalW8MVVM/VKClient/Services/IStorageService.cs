using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Services
{
    public interface IStorageService
        {
            Task<bool> CreateDirectory(string path);

            Task<Stream> CreateFile(string path);

            Task<string[]> GetFileNames(string dir = null);

            Task<object> GetImage(string path);

            Task<string> GetText(string fileName);

            Task<bool> IsDirectoryExists(string path);

            Task<bool> IsFileExists(string path);

            Task<Stream> ReadFile(string fileName);

            Task SaveStream(Stream stream, string fileName);

            Task SaveText(string text, string fileName);
        }

}
