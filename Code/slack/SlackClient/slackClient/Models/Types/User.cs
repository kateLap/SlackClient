namespace SlackClient.Models.Types
{
    public class User
    {
        public string Id { get; set; }      
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Color { get; set; }
        public UserProfile Profile { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsPrimaryOwner { get; set; }
        public bool IsRestricted { get; set; }
        public bool IsUltraRestricted { get; set; }       
    }
}