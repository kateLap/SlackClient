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
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the send message command.
        /// </summary>
        /// <value>
        /// The send message command.
        /// </value>
        public ICommand SendMessageCommand { protected set; get; }

        /// <summary>
        /// Gets or sets the messages list.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public ObservableCollection<ChatMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the Slack API class.
        /// </summary>
        /// <value>
        /// The slack.
        /// </value>
        public SlackApi Slack { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesListViewModel"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public MessagesListViewModel(Page page)
        {
            this._page = page;
            _message = new ChatMessage();
            Messages = new ObservableCollection<ChatMessage>();
            SendMessageCommand = new Command(SendMessage);
        }

        /// <summary>
        /// The message
        /// </summary>
        private readonly ChatMessage _message;

        /// <summary>
        /// Gets or sets the navigation of this app.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// The selected list view model
        /// </summary>
        EditTopicViewModel _selectedEdit;

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Gets or sets the messages list.
        /// </summary>
        /// <value>
        /// The messages list.
        /// </value>
        private IEnumerable<Message> MessagesList { get; set; }

        /// <summary>
        /// The chat name
        /// </summary>
        private string _chatName;

        public string ChatName
        {
            get => _chatName;
            set
            {
                if (_chatName == value) return;
                _chatName = value;
                OnPropertyChanged("ChatName");
            }
        }

        /// <summary>
        /// The chat created time
        /// </summary>
        private string _chatCreatedTime;

        public string ChatCreatedTime
        {
            get => _chatCreatedTime;
            set
            {
                if (_chatCreatedTime == value) return;
                _chatCreatedTime = value;
                OnPropertyChanged("ChatCreatedTime");
            }
        }

        /// <summary>
        /// The chat topic
        /// </summary>
        private string _chatTopic;

        public string ChatTopic
        {
            get => _chatTopic;
            set
            {
                if (_chatTopic == value) return;
                _chatTopic = value;
                OnPropertyChanged("ChatTopic");
            }
        }

        /// <summary>
        /// The chat identifier
        /// </summary>
        private string _chatId;

        public string ChatId
        {
            get => _chatId;
            set
            {
                if (_chatId == value) return;
                _chatId = value;
                OnPropertyChanged("ChatId");
            }
        }

        /// <summary>
        /// The chats list view model
        /// </summary>
        private ChatsListViewModel _lvm;

        public ChatsListViewModel ListViewModel
        {
            get => _lvm;
            set
            {
                if (_lvm == value) return;
                _lvm = value;
                OnPropertyChanged("ListViewModel");
            }
        }

        /// <summary>
        /// The updating flag
        /// </summary>
        private bool _isUpdating=false;

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
        /// The text of the message
        /// </summary>
        private string _textMessage;

        public string TextMessage
        {
            get => _textMessage;
            set
            {
                _textMessage = value;
                OnPropertyChanged("TextMessage");
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        public async void SendMessage()
        {
            IsUpdating = true;
            var channels = (ChannelsListResponse)Slack.Response;

            try
            {
                await Slack.ChannelsList();
                var chan = channels.Channels.Single(x => x.Name == _chatName);
                var chanId = chan.Id;

                await Slack.ChatPostMessage(chanId, _textMessage, null, true);
                TextMessage = null;

                await Slack.ChannelsHistory(chanId);
                var channelsHistory = (ChannelsHistoryResponse)Slack.Response;

                MessagesList = channelsHistory.Messages;

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                Messages.Clear();
                foreach (var currentMessage in MessagesList)
                {
                    _message.UserId = currentMessage.User;

                    var user = users.Members.Where(x => x.Id == _message.UserId);

                    _message.UserName = user.Count() != 0 ? user.First().Name : "";

                    _message.Time = currentMessage.Ts.ToString();
                    _message.Text = currentMessage.Text;

                    if (currentMessage.Type.Equals("message"))
                    {
                        Messages.Add(_message);
                    }

                    IsUpdating = false;
                }                
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await _page.DisplayAlert("Error!", e.Message, "Ok");
            }
            
        }

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
