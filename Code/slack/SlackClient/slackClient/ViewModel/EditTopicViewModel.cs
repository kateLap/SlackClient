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
        public event PropertyChangedEventHandler PropertyChanged;

        ChannelsListViewModel lvm;

        private Page page;

        public SlackAPI Slack { get; set; }

        public ICommand SetTopicCommand { protected set; get; }

        public EditTopicViewModel(Page page)
        {
            this.page = page;
            SetTopicCommand = new Command(SetTopic);
        }

        public ChannelsListViewModel ListViewModel
        {
            get => lvm;
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("MessagesListViewModel");
                }
            }
        }

        private string channelId;

        public string ChannelId
        {
            get => channelId;
            set
            {
                if (channelId != value)
                {
                    channelId = value;
                    OnPropertyChanged("ChannelId");
                }
            }
        }

        private string channelName;

        public string ChannelName
        {
            get => channelName;
            set
            {
                if (channelName != value)
                {
                    channelName = value;
                    OnPropertyChanged("ChannelName");
                }
            }
        }

        private string textTopic;
        public string TextTopic
        {
            get => textTopic;
            set
            {
                textTopic = value;
                OnPropertyChanged("TextTopic");
            }
        }

        private async void SetTopic()
        {
            try
            {
                await Slack.AuthTest();
                var auth = (AuthTestResponse)Slack.Response;
                var profileId = auth.UserId;

                await Slack.UsersList();
                var listResponse = (UsersListResponse)Slack.Response;

                bool isOwner = false;

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
                    var response = (SetTopicResponse)Slack.Response;
                }
                else
                {
                    throw new SlackClientException("You don't have administrator opportunities");
                }
            }
            catch (SlackClientException e)
            {
                await page.DisplayAlert("Error!", "You cannot change the topic of the channel", "Ok");
            }
        }


        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
