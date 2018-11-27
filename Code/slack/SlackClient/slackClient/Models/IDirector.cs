using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models
{
    public interface IDirector
    {
        /// <summary>
        /// Makes request for posting message
        /// </summary>
        void Make();
    }
}
