using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using SlackClient.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SlackClient.ViewModels
{
    public class ChatsListViewModel : SlackPageViewModel
    {
        public ObservableCollection<MessagesListViewModel> Chats { get; set; }

        public ICommand UpdateCommand { protected set; get; }

        MessagesListViewModel selectedChat;

        private Page page;

        public INavigation Navigation { get; set; }
        
        public ChatsListViewModel(Page page)
        {
            this.page = page;
            Chats = new ObservableCollection<MessagesListViewModel>();
            UpdateCommand = new Command(Update);
            Update();
        }

        public MessagesListViewModel SelectedChat
        {
            get => selectedChat;
            set
            {
                if (selectedChat != value)
                {
                    MessagesListViewModel tempChat = value;
                    tempChat.Messages.Clear();
                    tempChat.SendMessage();                    
                    selectedChat = null;
                    OnPropertyChanged("SelectedChat");
                    Navigation.PushAsync(new ChatMessagesPage(tempChat));
                }
            }
        }

        private bool isUpdating = false;
        public bool IsUpdating
        {
            get => isUpdating;
            set
            {
                isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        private async void Update()
        {
            try
            {
                IsUpdating = true;
                await slack.ChannelsList();

                var channels = (ChannelsListResponse)slack.Response;

                Chats.Clear();
                foreach (var currentChannel in channels.Channels)
                {
                    var newChat = new MessagesListViewModel(page) {
                        ChatName = currentChannel.Name,
                        Slack = this.slack,
                        ChatCreatedTime = currentChannel.Created.ToString(),
                        ChatTopic = currentChannel.Topic.Value,
                        ChatId = currentChannel.Id
                    };
                    Chats.Add(newChat);
                }
                IsUpdating = false;
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await page.DisplayAlert("Error!", e.Message, "Ok");
            }
        }
    }
}
