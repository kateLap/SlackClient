using System;

namespace SlackClient.Models.Types
{
    public class IMChannel
    {
        /// <summary>
        /// The identifier of the IM channel
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Will be true if this channel is IM channel
        /// </summary>
        public bool IsIm { get; set; }

        /// <summary>
        /// Defines chat user's ID 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Channel creation time
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        ///  Will be true if the other user's account has been disabled
        /// </summary>
        public bool IsUserDeleted { get; set; }

        /// <summary>
        ///  Shows if the IM channel is open
        /// </summary>
        public bool? IsOpen { get; set; }

        /// <summary>
        /// The timestamp for the last message the calling user has read in this channel
        /// </summary>
        public DateTime? LastRead { get; set; }

        /// <summary>
        /// A count of messages that the calling user has yet to read
        /// </summary>
        public int? UnreadCount { get; set; }

        /// <summary>
        /// The latest message in the channel
        /// </summary>
        public Message Latest { get; set; }
    }
}
