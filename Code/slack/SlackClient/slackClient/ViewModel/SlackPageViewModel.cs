using SlackClient.Models;
using System.ComponentModel;

namespace SlackClient.ViewModels
{
    public class SlackPageViewModel : INotifyPropertyChanged
    {
        protected SlackAPI slack;

        public event PropertyChangedEventHandler PropertyChanged;

        public SlackPageViewModel()
        {
            slack = new SlackAPI("");
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
