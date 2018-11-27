using System;

namespace SlackClient.Models.Types
{
    public class Message
    {
        /// <summary>
        /// ID of the user who sent this message
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The text of the message
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The type of the message
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Defines a subtype of the message
        /// </summary>
        public string Subtype { get; set; }

        /// <summary>
        /// The time of sending this message
        /// </summary>
        public DateTime Ts { get; set; }
    }
}
