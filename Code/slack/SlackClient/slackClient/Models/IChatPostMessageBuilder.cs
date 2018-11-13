using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models
{
    public interface IChatPostMessageBuilder
    {
        void BuildUserName(string name);

        void BuildAsUser(bool asUser);

        void BuildUnfurlLinks(bool isUnfurl);

        void BuildLinkNames(bool isAvailable);

        void BuildUnfurlMedia(bool isAvailable);

        void BuildIconUrl(string url);

        void BuildIconEmoji(string emoji);
    }
}
