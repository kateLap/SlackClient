using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

using SlackClient.Models;
using SlackClient.Models.Response;

using Xamarin.Forms;

namespace SlackClient.ViewModels
{

    public class EditTopicViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the class of Slack API.
        /// </summary>
        public SlackApi Slack { get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Gets or sets the set topic command.
        /// </summary>
        /// <value>
        /// The set topic command.
        /// </value>
        public ICommand SetTopicCommand { protected set; get; }

        /// <summary>
        /// The channels list view model
        /// </summary>
        ChannelsListViewModel _lvm;

        /// <summary>
        /// Gets or sets the list view model.
        /// </summary>
        /// <value>
        /// The ListView model.
        /// </value>
        public ChannelsListViewModel ListViewModel
        {
            get => _lvm;
            set
            {
                if (_lvm == value) return;
                _lvm = value;
                OnPropertyChanged("MessagesListViewModel");
            }
        }

        /// <summary>
        /// The channel identifier
        /// </summary>
        private string _channelId;

        public string ChannelId
        {
            get => _channelId;
            set
            {
                if (_channelId == value) return;
                _channelId = value;
                OnPropertyChanged("ChannelId");
            }
        }

        /// <summary>
        /// The channel name
        /// </summary>
        private string _channelName;

        public string ChannelName
        {
            get => _channelName;
            set
            {
                if (_channelName == value) return;
                _channelName = value;
                OnPropertyChanged("ChannelName");
            }
        }

        /// <summary>
        /// The topic text to set
        /// </summary>
        private string _textTopic;

        public string TextTopic
        {
            get => _textTopic;
            set
            {
                _textTopic = value;
                OnPropertyChanged("TextTopic");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTopicViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public EditTopicViewModel(Page page)
        {
            this._page = page;
            SetTopicCommand = new Command(SetTopic);
        }

        /// <summary>
        /// Sets the topic of a channel.
        /// </summary>
        /// <exception cref="SlackClient.Models.SlackClientException">You don't have administrator opportunities</exception>
        private async void SetTopic()
        {
            try
            {
                await Slack.AuthTest();
                var auth = (AuthTestResponse) Slack.Response;
                var profileId = auth.UserId;

                await Slack.UsersList();
                var listResponse = (UsersListResponse) Slack.Response;

                var isOwner = false;

                foreach (var user in listResponse.Members)
                {
                    if (user.Id == profileId)
                    {
                        isOwner = user.IsOwner;
                    }
                }

                if (isOwner)
                {
                    await Slack.ChannelsSetTopic(ChannelId, TextTopic);
                    var response = (SetTopicResponse) Slack.Response;
                }
                else
                {
                    throw new SlackClientException("You don't have administrator opportunities");
                }
            }
            catch (SlackClientException e)
            {
                await _page.DisplayAlert("Error!", "You cannot change the topic of the channel", "Ok");
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
