using System.Collections.Generic;

namespace SlackClient.Models
{
    public class DefaultChatPostMessageBuilder : IChatPostMessageBuilder
    {
        public DefaultChatPostMessageBuilder(string channelId, string text)
        {
            //TODO: В модели ChatPostMessage создать по свойству на каждый параметр.
            //TODO: Обязательные параметры инициализировать через конструктор, необязательные - через проперти.
            ChatPostMessage = new List<KeyValuePair<string, string>>(){
                // Required arguments
                Pair("channel", channelId),
                Pair("text", text)
            };
        }

        // TODO: Сделать отдельную модель для ChatPostMessage
        public ICollection<KeyValuePair<string, string>> ChatPostMessage { get; }
        
        public void BuildUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ChatPostMessage.Add(Pair("username", name));
            }
        }

        public void BuildAsUser(bool asUser)
        {
            ChatPostMessage.Add(Pair("as_user", asUser));
        }

        public void BuildUnfurlLinks(bool isUnfurl)
        {
            ChatPostMessage.Add(Pair("unfurl_links", isUnfurl));
        }

        public void BuildLinkNames(bool isAvailable)
        {
            ChatPostMessage.Add(Pair("unfurl_links", isAvailable));
        }

        public void BuildUnfurlMedia(bool isAvailable)
        {
           ChatPostMessage.Add(Pair("unfurl_links", isAvailable));
        }

        public void BuildIconUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                ChatPostMessage.Add(Pair("icon_url", url));
            }
        }

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
