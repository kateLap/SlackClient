using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using System.Collections.Generic;

namespace SlackClient.ViewModels
{
    public class MessagesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SendMessageCommand { protected set; get; }

        public ObservableCollection<ChatMessage> Messages { get; set; }
        private ChatMessage message;

        ChatsListViewModel lvm;

        public INavigation Navigation { get; set; }

        EditTopicViewModel selectedEdit;//

        private string chatName;
        private string chatCreatedTime;
        private string chatTopic;
        private string chatId;

        private readonly Page page;

        IEnumerable<Message> MessagesList { get; set; }

        public SlackAPI Slack { get; set; }

        public string ChatName
        {
            get => chatName;
            set
            {
                if (chatName != value)
                {
                    chatName = value;
                    OnPropertyChanged("ChatName");
                }
            }
        }
        public string ChatCreatedTime
        {
            get => chatCreatedTime;
            set
            {
                if (chatCreatedTime != value)
                {
                    chatCreatedTime = value;
                    OnPropertyChanged("ChatCreatedTime");
                }
            }
        }
        public string ChatTopic
        {
            get => chatTopic;
            set
            {
                if (chatTopic != value)
                {
                    chatTopic = value;
                    OnPropertyChanged("ChatTopic");
                }
            }
        }

        public string ChatId
        {
            get => chatId;
            set
            {
                if (chatId != value)
                {
                    chatId = value;
                    OnPropertyChanged("ChatId");
                }
            }
        }

        public MessagesListViewModel(Page page)
        {
            this.page = page;
            message = new ChatMessage();
            Messages = new ObservableCollection<ChatMessage>();
            SendMessageCommand = new Command(SendMessage);//
        }

        public ChatsListViewModel ListViewModel
        {
            get => lvm;
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }

        private bool isUpdating=false;
        public bool IsUpdating
        {
            get => isUpdating;
            set
            {
                isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        private string textMessage;
        public string TextMessage
        {
            get => textMessage;
            set
            {
                textMessage = value;
                OnPropertyChanged("TextMessage");
            }
        }

       /* private async void EditTopic()
        {
            EditTopicViewModel tempChat = value;
            selectedChat = null;
            OnPropertyChanged("SelectedChat");
            Navigation.PushAsync(new ChatMessagesPage(tempChat));
            // EditTopicViewModel tempEdit = new EditTopicViewModel();
            // tempEdit.TextTopic = String.Empty;
            // Navigation.PushAsync(new EditTopicPage(tempChat));
        }*/


        public async void SendMessage()
        {
            IsUpdating = true;
            try{
                await Slack.ChannelsList();
                ChannelsListResponse channels = (ChannelsListResponse)Slack.Response;
                var chan = channels.Channels.Where(x => x.Name == chatName).Single();
                var chanId = chan.Id;


                await Slack.ChatPostMessage(chanId, textMessage, null, true);
                TextMessage = null;

                await Slack.ChannelsHistory(chanId);
                ChannelsHistoryResponse channelsHistory = (ChannelsHistoryResponse)Slack.Response;

                MessagesList = channelsHistory.Messages;//.Reverse();

                await Slack.UsersList();
                UsersListResponse users = (UsersListResponse)Slack.Response;

                Messages.Clear();
                foreach (Message currentMessage in MessagesList)
                {
                    message.UserId = currentMessage.User;

                    var user = users.Members.Where(x => x.Id == message.UserId);

                    if (user.Count() != 0)
                        message.UserName = user.First().Name;
                    else
                        message.UserName = "";

                    message.Time = currentMessage.Ts.ToString();
                    message.Text = currentMessage.Text;

                    if (currentMessage.Type.Equals("message"))
                    {
                        Messages.Add(message);
                    }

                    IsUpdating = false;
                }
                
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await page.DisplayAlert("Error!", e.Message, "Ok");
            }
            
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
