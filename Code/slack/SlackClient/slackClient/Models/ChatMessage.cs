using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models
{
    public class ChatMessage
    {
        public string Text { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string Time { get; set; }
    }
}
