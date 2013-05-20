using System;

namespace VKClient.Models
{
    namespace Entities
    {
        public class Photo : IEntity
        {
            public long PhotoId { get; set; }
            public long AlbumId { get; set; }
            public long OwnerId { get; set; }
            public long Width { get; set; }
            public long Height { get; set; }
            public Uri Source { get; set; }
            public Uri SourceBig { get; set; }
            public Uri SourceSmall { get; set; }
            public Uri SourceXBig { get; set; }
            public Uri SourceX2Big { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
        }
    }
}
