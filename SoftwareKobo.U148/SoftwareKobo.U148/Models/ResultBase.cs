using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class ResultBase<T>
    {
        [JsonProperty("code")]
        public int Code
        {
            get;
            set;
        }

        [JsonProperty("msg")]
        public string Message
        {
            get;
            set;
        }

        [JsonProperty("data")]
        public T Data
        {
            get;
            set;
        }
    }
}