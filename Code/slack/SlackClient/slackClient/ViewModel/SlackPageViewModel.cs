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
            slack = new SlackAPI("xoxp-341336893090-341336893346-357285338209-b9aa7223b6a2182e189ec65172f4f2b1");
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
