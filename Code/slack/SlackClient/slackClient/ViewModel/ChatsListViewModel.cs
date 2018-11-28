using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using SlackClient.Views;

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

using Xamarin.Forms;

namespace SlackClient.ViewModels
{
    public class ChatsListViewModel : SlackPageViewModel
    {
        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Gets or sets the chats list.
        /// </summary>
        public ObservableCollection<MessagesListViewModel> Chats { get; set; }

        /// <summary>
        /// Gets or sets the update command.
        /// </summary>
        public ICommand UpdateCommand { protected set; get; }

        /// <summary>
        /// Gets or sets the navigation of the app.
        /// </summary>
        public INavigation Navigation { get; set; }
        
        /// <summary>
        /// The selected chat
        /// </summary>
        private MessagesListViewModel _selectedChat;

        /// <summary>
        /// Gets or sets the selected chat.
        /// </summary>
        public MessagesListViewModel SelectedChat
        {
            get => _selectedChat;
            set
            {
                if (_selectedChat != value)
                {
                    MessagesListViewModel tempChat = value;
                    tempChat.Messages.Clear();
                    tempChat.SendMessage();                    
                    _selectedChat = null;
                    OnPropertyChanged("SelectedChat");
                    Navigation.PushAsync(new ChatMessagesPage(tempChat));
                }
            }
        }

        /// <summary>
        /// The page updating flag
        /// </summary>
        private bool _isUpdating = false;

        public bool IsUpdating
        {
            get => _isUpdating;
            set
            {
                _isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatsListViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public ChatsListViewModel(Page page)
        {
            this._page = page;
            Chats = new ObservableCollection<MessagesListViewModel>();
            UpdateCommand = new Command(Update);
            Update();
        }

        /// <summary>
        /// Updates this page.
        /// </summary>
        private async void Update()
        {
            try
            {
                IsUpdating = true;
                await Slack.ChannelsList();

                var channels = (ChannelsListResponse) Slack.Response;

                Chats.Clear();
                foreach (var currentChannel in channels.Channels)
                {
                    var newChat = new MessagesListViewModel(_page)
                    {
                        ChatName = currentChannel.Name,
                        Slack = this.Slack,
                        ChatCreatedTime = currentChannel.Created.ToString(CultureInfo.InvariantCulture),
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
                await _page.DisplayAlert("Error!", e.Message, "Ok");
            }
        }
    }
}
