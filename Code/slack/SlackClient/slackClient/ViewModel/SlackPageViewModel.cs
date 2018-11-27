using SlackClient.Models;
using System.ComponentModel;

namespace SlackClient.ViewModels
{
    /// <summary>
    /// The main page for application pages
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SlackPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The Slack API class
        /// </summary>
        protected SlackApi Slack;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackPageViewModel"/> class.
        /// </summary>
        public SlackPageViewModel()
        {
            Slack = new SlackApi("");
        }

        /// <summary>
        /// Called when property changed]
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
