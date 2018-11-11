using System;

namespace SlackClient.Models.Types
{
    public class Message
    {
        public string User { get; set; }
        public string Text { get; set; }
        //public string Type { get; set; }
        //public string Subtype { get; set; }
        public DateTime Ts { get; set; }
    }
}
