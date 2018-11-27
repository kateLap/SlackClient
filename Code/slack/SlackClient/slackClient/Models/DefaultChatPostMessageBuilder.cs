using System.Collections.Generic;

namespace SlackClient.Models
{
    /// <summary>
    /// This class defines default default builder for sending message
    /// </summary>
    public class DefaultChatPostMessageBuilder : IChatPostMessageBuilder
    {
        /// <summary>
        /// Sets required parameters for the method Post message
        /// </summary>
        /// <param name="channelId">ID of the channel which receives the message</param>
        /// <param name="text">The text of the channel</param>
        public DefaultChatPostMessageBuilder(string channelId, string text)
        {
            ChatPostMessage = new List<KeyValuePair<string, string>>()
            {
                Pair("channel", channelId),
                Pair("text", text)
            };
        }

        /// <summary>
        /// Collection for parameters for Post message method
        /// </summary>
        public ICollection<KeyValuePair<string, string>> ChatPostMessage { get; }
        
        /// <summary>
        /// Sets user's name
        /// </summary>
        /// <param name="name">user's name</param>
        public void BuildUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ChatPostMessage.Add(Pair("username", name));
            }
        }

        /// <summary>
        /// If it's true the message will be post from user's name
        /// </summary>
        public void BuildAsUser(bool asUser)
        {
            ChatPostMessage.Add(Pair("as_user", asUser));
        }

        /// <summary>
        /// Pass true to enable unfurling of primarily text-based content
        /// </summary>
        public void BuildUnfurlLinks(bool isUnfurl)
        {
            ChatPostMessage.Add(Pair("unfurl_links", isUnfurl));
        }

        /// <summary>
        /// Find and link channel names and usernames
        /// </summary>
        public void BuildLinkNames(string linkNames)
        {
            ChatPostMessage.Add(Pair("link_names", linkNames));
        }

        /// <summary>
        /// Pass false to disable unfurling of media content
        /// </summary>
        public void BuildUnfurlMedia(bool unfurlMedia)
        {
            ChatPostMessage.Add(Pair("unfurl_media", unfurlMedia));
        }

        /// <summary> 	
        /// URL to an image to use as the icon for this message
        /// </summary>
        public void BuildIconUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                ChatPostMessage.Add(Pair("icon_url", url));
            }
        }

        /// <summary>
        ///Emoji to use as the icon for this message
        /// </summary>
        public void BuildIconEmoji(string emoji)
        {
            if (!string.IsNullOrWhiteSpace(emoji))
            {
                ChatPostMessage.Add(Pair("icon_emoji", emoji));
            }
        }

        protected static KeyValuePair<string, string> Pair(string key, object value)
        {
            return Pair(key, value.ToString());
        }

        protected static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
    }
}
