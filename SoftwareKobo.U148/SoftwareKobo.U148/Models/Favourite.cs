using Newtonsoft.Json;
using SoftwareKobo.UniversalToolkit.Converters.Json;
using System;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class Favourite
    {
        [JsonProperty("id")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("uid")]
        public int Uid
        {
            get;
            set;
        }

        [JsonProperty("category")]
        public FeedCategory Category
        {
            get;
            set;
        }

        [JsonProperty("aid")]
        public int Aid
        {
            get;
            set;
        }

        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("url")]
        public string Url
        {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("is_stock")]
        public int IsStock
        {
            get;
            set;
        }

        [JsonProperty("is_private")]
        public bool IsPrivate
        {
            get;
            set;
        }

        [JsonProperty("create_time")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [JsonProperty("update_time")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime UpdateTime
        {
            get;
            set;
        }

        [JsonProperty("status")]
        public int Status
        {
            get;
            set;
        }

        /*
        [JsonProperty("favourite_category")]
        public object FavouriteCategory
        {
            get;
            set;
        }
        */
    }
}