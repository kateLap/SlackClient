using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class UsersListResponse : SlackResponse
    {
        /// <summary>
        /// Gets or sets the list of users from current workspace
        /// </summary>
        public IEnumerable<User> Members { get; set; }
    }
}