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
            slack = new SlackAPI("xoxp-341336893090-341336893346-488785318771-dbac2fd6d0662bc21bfe3afee85f10f7");
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
