using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using System.Collections.Generic;
using System.Globalization;

namespace SlackClient.ViewModels
{
    public class IMMessagesListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the messages list.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public ObservableCollection<ChatMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the messages models list.
        /// </summary>
        /// <value>
        /// The messages list.
        /// </value>
        private IEnumerable<Message> MessagesList { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        private readonly ChatMessage _message;

        /// <summary>
        /// Gets or sets the Slack API class.
        /// </summary>
        public SlackApi Slack { get; set; }

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// The user of the current dialog
        /// </summary>
        private string _imUser;

        public string ImUser
        {
            get => _imUser;
            set
            {
                if (_imUser == value) return;

                _imUser = value;

                OnPropertyChanged("IMUser");
            }
        }

        /// <summary>
        /// The dialog created time
        /// </summary>
        private string _imCreatedTime;

        public string ImCreatedTime
        {
            get => _imCreatedTime;
            set
            {
                if (_imCreatedTime == value) return;

                _imCreatedTime = value;

                OnPropertyChanged("IMCreatedTime");
            }
        }

        /// <summary>
        /// The dialog identifier
        /// </summary>
        private string _imId;

        public string IMId
        {
            get => _imId;
            set
            {
                if (_imId == value) return;

                _imId = value;

                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// The im user image
        /// </summary>
        private string _imUserImage;

        public string IMUserImage
        {
            get => _imUserImage;
            set
            {
                if (_imUserImage == value) return;

                _imUserImage = value;

                OnPropertyChanged("UserImage");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IMMessagesListViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public IMMessagesListViewModel(Page page)
        {
            this._page = page;

            _message = new ChatMessage();

            Messages = new ObservableCollection<ChatMessage>();

            this.SendMessageCommand = new Command(SendMessage);
        }

        /// <summary>
        /// The list view model
        /// </summary>
        private IMListViewModel _lvm;

        public IMListViewModel ListViewModel
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
        /// Gets or sets the send message command.
        /// </summary>
        /// <value>
        /// The send message command.
        /// </value>
        public ICommand SendMessageCommand { protected set; get; }

        /// <summary>
        /// The updating flag
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
        /// Sends the message to an user.
        /// </summary>
        public async void SendMessage()
        {
            try
            {
                IsUpdating = true;

                await Slack.IMHistory(IMId);
                var imMessages = (ChannelsHistoryResponse)Slack.Response;
                MessagesList = imMessages.Messages;

                if (_textMessage != null)
                {
                     var builder = new DefaultChatPostMessageBuilder(IMId, _textMessage);
                     var director = new ChatPostMessageDirector(builder);
                     await Slack.Send(builder.ChatPostMessage);

                    TextMessage = null;
                }

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                Messages.Clear();
                foreach (var currentMessage in MessagesList)
                {
                    _message.UserId = currentMessage.User;
                    var user = users.Members.Where(x => x.Id == _message.UserId);                   

                    if (user.Count() != 0)
                    {
                        _message.UserName = user.First().Profile.RealName;
                        _message.UserImage = user.First().Profile.Image72;
                    }
                    else
                    {
                        _message.UserName = "";
                    }

                    _message.Time = currentMessage.Ts.ToString();
                    _message.Text = currentMessage.Text;

                    if (currentMessage.Type.Equals("message"))
                    {
                        Messages.Add(_message);
                    }
                }
                IsUpdating = false;
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
