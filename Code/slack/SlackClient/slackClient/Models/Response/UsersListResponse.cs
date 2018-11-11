using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class UsersListResponse : SlackResponse
    {
        public IEnumerable<User> Members { get; set; }
    }
}