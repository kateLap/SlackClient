using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models.Types
{
    public abstract class AbstractChannel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }      
        public string Creator { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<string> Members { get; set; }
        public ValueInfo Topic { get; set; }
        public ValueInfo Purpose { get; set; }
        public DateTime? LastRead { get; set; }
        public Message Latest { get; set; }
        public int? UnreadCount { get; set; }
        public int? UnreadCountDisplay { get; set; }
    }
}
