using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models.Types
{
using System.Runtime.Serialization;

[DataContract]
public enum Error
{
        /// <summary>
        /// Default value when not assigned.
        /// </summary>
        None,

        /// <summary>
        /// Authentication token is for a deleted user or team.
        /// </summary>
        [EnumMember(Value = "account_inactive")]
        AccountInactive,

        /// <summary>
        /// Authenticated user does not have permission to update this message.
        /// </summary>
        [EnumMember(Value = "cant_update_message")]
        CantUpdateMessage,

        /// <summary>
        /// Authenticated user does not have permission to delete this message.
        /// </summary>
        [EnumMember(Value = "cant_delete_message")]
        CantDeleteMessage,

        /// <summary>
        /// Value passed for channel was invalid.
        /// </summary>
        [EnumMember(Value = "channel_not_found")]
        ChannelNotFound,

        /// <summary>
        /// The message cannot be edited due to the team message edit settings
        /// </summary>
        [EnumMember(Value = "edit_window_closed")]
        EditWindowClosed,

        /// <summary>
        /// Invalid authentication token.
        /// </summary>
        [EnumMember(Value = "invalid_auth")]
        InvalidAuth,

        /// <summary>
        /// Value passed for presence was invalid.
        /// </summary>
        [EnumMember(Value = "invalid_presence")]
        InvalidPresense,

        /// <summary>
        /// Private group has been archived
        /// </summary>
        [EnumMember(Value = "is_archived")]
        IsArchived,

        /// <summary>
        /// No message exists with the requested timestamp.
        /// </summary>
        [EnumMember(Value = "message_not_found")]
        MessageNotFound,

        /// <summary>
        /// Message text is too long
        /// </summary>
        [EnumMember(Value = "msg_too_long")]
        MessageTooLong,

        /// <summary>
        /// No message text provided.
        /// </summary>
        [EnumMember(Value = "no_text")]
        NoText,

        /// <summary>
        /// No authentication token provided.
        /// </summary>
        [EnumMember(Value = "not_authed")]
        NotAuthed,

        /// <summary>
        /// Caller is not a member of the channel.
        /// </summary>
        [EnumMember(Value = "not_in_channel")]
        NotInChannel,

        /// <summary>
        /// Topic or Purpose was longer than 250 characters.
        /// </summary>
        [EnumMember(Value = "too_long")]
        TooLong,

        /// <summary>
        /// Calling user does not own this DM channel.
        /// </summary>
        [EnumMember(Value = "user_does_not_own_channel")]
        UserDoesNotOwnChannel,

        /// <summary>
        /// This method cannot be called by a restricted user or single channel guest.
        /// </summary>
        [EnumMember(Value = "user_is_restricted")]
        UserIsRestricted,

        /// <summary>
        /// Value passed for user was invalid.
        /// </summary>
        [EnumMember(Value = "user_not_found")]
        UserNotFound,

        /// <summary>
        /// The requested user is not visible to the calling user.
        /// </summary>
        [EnumMember(Value = "user_not_visible")]
        UserNotVisible,
    }
}
