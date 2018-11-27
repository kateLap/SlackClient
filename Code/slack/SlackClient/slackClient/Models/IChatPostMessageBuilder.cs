using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models
{
    /// <summary>
    /// Interface to implement parameters of Post message method
    /// </summary>
    public interface IChatPostMessageBuilder
    {
        /// <summary>
        /// Sets user's name
        /// </summary>
        void BuildUserName(string name);

        /// <summary>
        /// If it's true the message will be post from user's name
        /// </summary>
        void BuildAsUser(bool asUser);

        /// <summary>
        /// Pass true to enable unfurling of primarily text-based content
        /// </summary>
        void BuildUnfurlLinks(bool isUnfurl);

        /// <summary>
        /// Find and link channel names and usernames
        /// </summary>
        void BuildLinkNames(bool isAvailable);

        /// <summary>
        /// Pass false to disable unfurling of media content
        /// </summary>
        void BuildUnfurlMedia(bool isAvailable);

        /// <summary> 	
        /// URL to an image to use as the icon for this message
        /// </summary>
        void BuildIconUrl(string url);

        /// <summary>
        ///Emoji to use as the icon for this message
        /// </summary>
        void BuildIconEmoji(string emoji);
    }
}