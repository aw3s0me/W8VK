using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class Video
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public TimeSpan Duration
        {
            get;
            set;
        }

        public Dictionary<string, string> Files
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string ImageMedium
        {
            get;
            set;
        }

        public string ImageSmall
        {
            get;
            set;
        }

        public string Link
        {
            get;
            set;
        }

        public string OwnerId
        {
            get;
            set;
        }

        public string Player
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public Video()
        {
        }
    }
}
