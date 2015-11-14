using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class DataResultBase<T> : ResultBase
    {
        [JsonProperty("data")]
        public T Data
        {
            get;
            set;
        }
    }
}