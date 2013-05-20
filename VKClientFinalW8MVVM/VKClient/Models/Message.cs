using System;

namespace VKClient.Models
{
    namespace Entities
    {
        public class Message : IEntity
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Uid { get; set; }
            public string MsgId { get; set; }
            public bool IsOutMsg { get; set; }
            public bool IsNewMsg { get; set; }
            public DateTime Date { get; set; }
        }
    }
}

