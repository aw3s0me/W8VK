using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKClient.Models.Entities;

namespace VKClient.Models
{
    public class Group : IEntity
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public Uri PhotoUri
        {
            get;
            set;
        }

        public Group()
        {
        }
    }
}
