namespace TVShowTracker.Webapi.Application.Options
{
    public class AppKeysOption
    {
        public Jwt jwt { get; set; }
        public Google google { get; set; }
        public Facebook facebook { get; set; }
    }
    public class Jwt
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
    public class Google
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
    public class Facebook
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }
}
