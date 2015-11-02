using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class UserInfo
    {
        [JsonProperty("icon")]
        public string Icon
        {
            get;
            set;
        }

        [JsonProperty("nickname")]
        public string NickName
        {
            get;
            set;
        }

        [JsonProperty("sex")]
        public Gender Gender
        {
            get;
            set;
        }

        [JsonProperty("token")]
        public string Token
        {
            get;
            set;
        }
    }
}