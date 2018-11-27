using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models
{
    public class ChatMessage
    {
        /// <summary>
        /// Gets or sets the text of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user's ID who sent this message
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user's name who sent this message
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's image who sent this message
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// Gets or sets the time of sending this message
        /// </summary>
        public string Time { get; set; }
    }
}
