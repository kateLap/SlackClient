using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models.Types
{
    public abstract class AbstractChannel
    {
        /// <summary>
        /// The identifier of the channel
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Indicates the name of the channel
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Channel creation time
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The user ID of the member that created this channel
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Will be true if the channel is archived.
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// A list of user IDs for all users in this channel
        /// </summary>
        public IEnumerable<string> Members { get; set; }

        /// <summary>
        /// Defines the topic of the channel
        /// </summary>
        public ValueInfo Topic { get; set; }

        /// <summary>
        /// Defines the purpose of the channel
        /// </summary>
        public ValueInfo Purpose { get; set; }

        /// <summary>
        ///  The timestamp for the last message the calling user has read in this channel
        /// </summary>
        public DateTime? LastRead { get; set; }


        /// <summary>
        /// The latest message in the channel
        /// </summary>
        public Message Latest { get; set; }

        /// <summary>
        ///  A full count of visible messages that the calling user has yet to read
        /// </summary>
        public int? UnreadCount { get; set; }

        /// <summary>
        ///  A count of messages that the calling user has yet to read that
        ///  matter to them (this means it excludes things like join/leave messages).
        /// </summary>
        public int? UnreadCountDisplay { get; set; }
    }
}
