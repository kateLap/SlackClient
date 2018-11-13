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
            slack = new SlackAPI("xoxp-341336893090-354054396951-459723576086-16168c67e26a3d4c5c3a1eafd45daae0");
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
