using System;

namespace SlackClient.Models.Types
{
    public class ValueInfo
    {
        public string Value { get; set; }
        public string Creator { get; set; }
        public DateTime LastSet { get; set; }
    }
}