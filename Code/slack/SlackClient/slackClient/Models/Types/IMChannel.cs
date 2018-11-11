using System;

namespace SlackClient.Models.Types
{
    public class IMChannel
    {
        public string Id { get; set; }
        public bool IsIm { get; set; }
        public string User { get; set; }
        public DateTime Created { get; set; }
        public bool IsUserDeleted { get; set; }
        public bool? IsOpen { get; set; }
        public DateTime? LastRead { get; set; }
        public int? UnreadCount { get; set; }
        public Message Latest { get; set; }
    }
}
