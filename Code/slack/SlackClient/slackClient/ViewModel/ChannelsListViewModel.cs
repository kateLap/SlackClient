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
        public ObservableCollection<EditTopicViewModel> Channels { get; set; }

        public ICommand UpdateCommand { protected set; get; }

        private readonly Page page;

        public INavigation Navigation { get; set; }

        public ChannelsListViewModel(Page page)
        {
            this.page = page;
            Channels = new ObservableCollection<EditTopicViewModel>();
            UpdateCommand = new Command(Update);
            Update();
        }

        EditTopicViewModel selectedChannel;

        public EditTopicViewModel SelectedChannel
        {
            get => selectedChannel;
            set
            {
                if (selectedChannel != value)
                {
                    EditTopicViewModel tempChannel = value;

                    selectedChannel = null;
                    OnPropertyChanged("SelectedChannel");
                    Navigation.PushAsync(new EditTopicPage(tempChannel));
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

                Channels.Clear();
                foreach (Channel currentChannel in channels.Channels)
                {
                    var newChat = new EditTopicViewModel(page)
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
                await page.DisplayAlert("Error!", e.Message, "Ok");
            }
        }
    }
}
