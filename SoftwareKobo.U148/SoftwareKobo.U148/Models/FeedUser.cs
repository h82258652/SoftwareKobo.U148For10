using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class FeedUser
    {
        [JsonProperty("nickname")]
        public string NickName
        {
            get;
            set;
        }

        [JsonProperty("alias")]
        public string Alias
        {
            get;
            set;
        }

        [JsonProperty("icon")]
        public string Icon
        {
            get;
            set;
        }
    }
}