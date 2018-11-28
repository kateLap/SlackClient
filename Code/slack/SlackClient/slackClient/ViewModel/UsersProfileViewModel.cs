using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;

using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;

namespace SlackClient.ViewModels
{
    public class UsersProfileViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Gets or sets the channels list.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public ObservableCollection<UsersChannel> Channels { get; set; }

        /// <summary>
        /// Gets or sets the Slack API class.
        /// </summary>
        /// <value>
        /// The Slack API class.
        /// </value>
        public SlackApi Slack { private get; set; }

        /// <summary>
        /// Gets or sets the update channels command.
        /// </summary>
        /// <value>
        /// The update channels command.
        /// </value>
        public ICommand UpdateChannelsCommand { get; protected set; }

        /// <summary>
        /// The selected channel
        /// </summary>
        private UsersChannel _selectedChannel;

        public UsersChannel SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                if (_selectedChannel == value) return;

                var tempChat = value;

                Kick(tempChat);

                _selectedChannel = null;

                OnPropertyChanged("SelectedChannel");
            }
        }

        /// <summary>
        /// The user identifier
        /// </summary>
        private string _userId;

        public string UserId
        {
            get => _userId;
            set
            {
                if (_userId == value) return;
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        /// <summary>
        /// The user email
        /// </summary>
        private string _userEmail;

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                if (_userEmail == value) return;
                _userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }

        /// <summary>
        /// The user name
        /// </summary>
        private string _user;

        public string User
        {
            get => _user;
            set
            {
                if (_user == value) return;
                _user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// The user image
        /// </summary>
        private string _userImage;

        public string UserImage
        {
            get => _userImage;
            set
            {
                if (_userImage == value) return;
                _userImage = value;
                OnPropertyChanged("UserImage");
            }
        }

        /// <summary>
        /// The list view model
        /// </summary>
        private UsersListViewModel _lvm;

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

        /// <summary>
        /// The updating page
        /// </summary>
        private bool _isUpdating;

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
        /// Initializes a new instance of the <see cref="UsersProfileViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public UsersProfileViewModel(Page page)
        {
            this._page = page;

            Channels = new ObservableCollection<UsersChannel>();
            this.UpdateChannelsCommand = new Command(UpdateChannels);
        }

        /// <summary>
        /// Kicks someone from the specified channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public async void Kick(UsersChannel channel)
        {
            try
            {
                await Slack.ChannelsKick(channel.Id, _userId);
                var kickResponse = (SlackResponse) Slack.Response;
                UpdateChannels();
            }
            catch (SlackClientException e)
            {
                IsUpdating = false;
                await _page.DisplayAlert("Error!", $"You cannot kick user {User} from channel: {channel.Name}", "Ok");
            }
        }

        /// <summary>
        /// Updates the channels.
        /// </summary>
        public async void UpdateChannels()
        {
            try
            {
                IsUpdating = true;

                await Slack.ChannelsList();
                var channelsResponse = (ChannelsListResponse) Slack.Response;

                var channels = channelsResponse.Channels;

                var channelsList = (
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

