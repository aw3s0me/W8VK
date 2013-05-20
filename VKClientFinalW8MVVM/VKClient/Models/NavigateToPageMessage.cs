﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient.Models
{
    public class NavigateToPageMessage
    {
        public string Page
        {
            get;
            set;
        }
        public Dictionary<string, object> Parameters
        {
            get;
            set;
        }
    }
}
