using Newtonsoft.Json;
using SoftwareKobo.UniversalToolkit.Converters.Json;
using System;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class Feed
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

        [JsonProperty("title")]
        public string Title
        {
            get;
            set;
        }

        [JsonProperty("summary")]
        public string Summary
        {
            get;
            set;
        }

        [JsonProperty("tids")]
        public string Tids
        {
            get;
            set;
        }

        [JsonProperty("tags")]
        public string Tags
        {
            get;
            set;
        }

        [JsonProperty("pic_min")]
        public string PicMin
        {
            get;
            set;
        }

        [JsonProperty("pic_mid")]
        public string PicMid
        {
            get;
            set;
        }

        [JsonProperty("is_index")]
        public int IsIndex
        {
            get;
            set;
        }

        [JsonProperty("is_hot")]
        public int IsHot
        {
            get;
            set;
        }

        [JsonProperty("is_top")]
        public int IsTop
        {
            get;
            set;
        }

        [JsonProperty("star")]
        public int Star
        {
            get;
            set;
        }

        [JsonProperty("count_browse")]
        public int CountBrowse
        {
            get;
            set;
        }

        [JsonProperty("count_review")]
        public int CountReview
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

        [JsonProperty("start_time")]
        [JsonConverter(typeof(UnixTimestampConverter))]
        public DateTime StartTime
        {
            get;
            set;
        }

        [JsonProperty("usr")]
        public FeedUser User
        {
            get;
            set;
        }
    }
}