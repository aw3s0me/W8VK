﻿using System;

namespace VKModel
{
    namespace Entities
    {
        public class Photo : IEntity
        {
            public string PhotoId { get; set; }
            public string AlbumId { get; set; }
            public string OwnerId { get; set; }
            public string Source { get; set; }
            public string SourceBig { get; set; }
            public string SourceSmall { get; set; }
            public string SourceXBig { get; set; }
            public string SourceX2Big { get; set; }
            public DateTime Created { get; set; }
            //public DateTime Updated { get; set; }
        }
    }
}