using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using Xamarin.Forms;

namespace SlackClient.ViewModels
{
    public class UsersProfileViewModel : INotifyPropertyChanged
    {
        private UsersListViewModel _lvm;

        private readonly Page _page;

        private UsersChannel _selectedChannel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand UpdateChannelsCommand { get; protected set; }

        public ObservableCollection<UsersChannel> Channels { get; set; }

        public SlackAPI Slack { private get; set; }

        public UsersChannel SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                if (_selectedChannel != value)
                {
                    UsersChannel tempChat = value;

                    Kick(tempChat);

                    _selectedChannel = null;

                    OnPropertyChanged("SelectedChannel");
                }
            }
        }

        private string userId;

        public string UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }


        private string userEmail;

        public string UserEmail
        {
            get => userEmail;
            set
            {
                if (userEmail != value)
                {
                    userEmail = value;
                    OnPropertyChanged("UserEmail");
                }
            }
        }


        private string user;

        public string User
        {
            get => user;
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                }
            }
        }
        
        private string _userImage;

        public string UserImage
        {
            get => _userImage;
            set
            {
                if (_userImage != value)
                {
                    _userImage = value;
                    OnPropertyChanged("UserImage");
                }
            }
        }

        public UsersProfileViewModel(Page page)
        {
            this._page = page;

            Channels = new ObservableCollection<UsersChannel>();
            this.UpdateChannelsCommand = new Command(UpdateChannels);
        }

        public UsersListViewModel ListViewModel
        {
            get => _lvm;
            set
            {
                if (_lvm != value)
                {
                    _lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }

        private bool isUpdating;

        public bool IsUpdating
        {
            get => isUpdating;
            set
            {
                isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        public async void Kick(UsersChannel channel)
        {
            try
            {
                await Slack.ChannelsKick(channel.Id, userId);
                var kickResponse = (SlackResponse) Slack.Response;
                UpdateChannels();
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await _page.DisplayAlert("Error!", $"You cannot kick user {User} from channel: {channel.Name}", "Ok");
            }
        }

        public async void UpdateChannels()
        {
            try
            {
                IsUpdating = true;

                await Slack.ChannelsList();
                var channelsResponse = (ChannelsListResponse)Slack.Response;

                var channels = channelsResponse.Channels;

                List<Channel> channelsList = (
                    from item in channels
                    from member in item.Members
                    where member == UserId
                    select item)
                    .ToList();

                Channels.Clear();

                foreach (var item in channelsList)
                {
                    var message = new UsersChannel
                    {
                        Name = item.Name,
                        Id = item.Id
                    };
                    
                    Channels.Add(message);
                }

                
                IsUpdating = false;
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await _page.DisplayAlert("Error!", e.Message, "Ok");
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

