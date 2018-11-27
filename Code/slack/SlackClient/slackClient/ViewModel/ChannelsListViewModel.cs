using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using SlackClient.Views;
using Xamarin.Forms;

namespace SlackClient.ViewModels
{
    public class ChannelsListViewModel : SlackPageViewModel
    {
        /// <summary>
        /// Channels list
        /// </summary>
        public ObservableCollection<EditTopicViewModel> Channels { get; set; }

        /// <summary>
        /// Command to update current page
        /// </summary>
        public ICommand UpdateCommand { protected set; get; }

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Navigation of this app
        /// </summary>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelsListViewModel"/> class.
        /// </summary>
        public ChannelsListViewModel(Page page)
        {
            this._page = page;
            Channels = new ObservableCollection<EditTopicViewModel>();
            UpdateCommand = new Command(Update);
            Update();
        }

        /// <summary>
        /// The selected channel
        /// </summary>
        private EditTopicViewModel _selectedChannel;

        /// <summary>
        /// Gets or sets the selected channel.
        /// </summary>
        /// <value>
        /// The selected channel.
        /// </value>
        public EditTopicViewModel SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                if (_selectedChannel != value)
                {
                    EditTopicViewModel tempChannel = value;

                    _selectedChannel = null;
                    OnPropertyChanged("SelectedChannel");
                    Navigation.PushAsync(new EditTopicPage(tempChannel));
                }
            }
        }

        /// <summary>
        /// Updating page flag.
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
        /// Updates this page.
        /// </summary>
        private async void Update()
        {
            try
            {
                IsUpdating = true;
                await slack.ChannelsList();

                var channels = (ChannelsListResponse)slack.Response;

                Channels.Clear();
                foreach (var currentChannel in channels.Channels)
                {
                    var newChat = new EditTopicViewModel(_page)
                    {
                        TextTopic = currentChannel.Topic.Value,
                        ChannelName = currentChannel.Name,
                        ChannelId = currentChannel.Id,
                        Slack = slack
                    };
                    Channels.Add(newChat);
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
