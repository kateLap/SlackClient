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
    public class IMMessagesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ChatMessage> Messages { get; set; }

        IEnumerable<Message> MessagesList { get; set; }

        private ChatMessage message;

        IMListViewModel lvm;
        public SlackAPI Slack { get; set; }
        
        private string imUser;
        private string imCreatedTime;
        private string imId;
        private string imUserImage;

        private Page page;

        private string textMessage;

        public string IMUser
        {
            get { return imUser; }
            set
            {
                if (imUser != value)
                {
                    imUser = value;
                    OnPropertyChanged("IMUser");
                }
            }
        }
        public string IMCreatedTime
        {
            get { return imCreatedTime; }
            set
            {
                if (imCreatedTime != value)
                {
                    imCreatedTime = value;
                    OnPropertyChanged("IMCreatedTime");
                }
            }
        }
        public string IMId
        {
            get { return imId; }
            set
            {
                if (imId != value)
                {
                    imId = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string IMUserImage
        {
            get { return imUserImage; }
            set
            {
                if (imUserImage != value)
                {
                    imUserImage = value;
                    OnPropertyChanged("UserImage");
                }
            }
        }

        public IMMessagesListViewModel(Page page)
        {
            this.page = page;
            message = new ChatMessage();
            Messages = new ObservableCollection<ChatMessage>();
            this.SendMessageCommand = new Command(SendMessage);
        }

        public IMListViewModel ListViewModel
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

        public string TextMessage
        {
            get => textMessage;
            set
            {
                textMessage = value;
                OnPropertyChanged("TextMessage");
            }
        }

        public ICommand SendMessageCommand { protected set; get; }

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

        public async void SendMessage()
        {
            try
            {
                IsUpdating = true;

                await Slack.IMHistory(IMId);
                var imMessages = (ChannelsHistoryResponse)Slack.Response;
                MessagesList = imMessages.Messages;

                if (textMessage != null)
                {
                     var builder = new DefaultChatPostMessageBuilder(IMId, textMessage);
                     var director = new ChatPostMessageDirector(builder);
                     await Slack.Send(builder.ChatPostMessage);

                    TextMessage = null;
                }

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                Messages.Clear();
                foreach (Message currentMessage in MessagesList)
                {
                    message.UserId = currentMessage.User;
                    var user = users.Members.Where(x => x.Id == message.UserId);                   

                    if (user.Count() != 0)
                    {
                        message.UserName = user.First().Profile.RealName;
                        message.UserImage = user.First().Profile.Image72;
                    }
                    else
                    {
                        message.UserName = "";
                    }

                    message.Time = currentMessage.Ts.ToString();
                    message.Text = currentMessage.Text;

                    if (currentMessage.Type.Equals("message"))
                    {
                        Messages.Add(message);
                    }
                }
                IsUpdating = false;
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
