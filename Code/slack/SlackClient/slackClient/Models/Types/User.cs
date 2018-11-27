namespace SlackClient.Models.Types
{
    public class User
    {
        /// <summary>
        /// User's ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is deleted.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Defines the profile of this user
        /// </summary>
        public UserProfile Profile { get; set; }

        /// <summary>
        /// Will be true if this user is admin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Will be true if this user is owner
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Will be true if this user is primary owner
        /// </summary>
        public bool IsPrimaryOwner { get; set; }

        /// <summary>
        /// Will be true if this user is restricted
        /// </summary>
        public bool IsRestricted { get; set; }

        /// <summary>
        /// Will be true if this user is ultra restricted
        /// </summary>
        public bool IsUltraRestricted { get; set; }       
    }
}