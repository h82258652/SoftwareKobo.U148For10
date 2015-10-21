using Newtonsoft.Json;
using SoftwareKobo.UniversalToolkit.Converters.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class Comment
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

        [JsonProperty("aid")]
        public int Aid
        {
            get;
            set;
        }

        [JsonProperty("floor_no")]
        public int FloorNo
        {
            get;
            set;
        }

        [JsonProperty("ip")]
        public string IP
        {
            get;
            set;
        }

        [JsonProperty("area")]
        public string Area
        {
            get;
            set;
        }

        [JsonProperty("remote_port")]
        public int RemotePort
        {
            get;
            set;
        }

        [JsonProperty("contents")]
        public string Contents
        {
            get;
            set;
        }

        [JsonProperty("hashcode")]
        public string HashCode
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

        [JsonProperty("is_commend")]
        public int IsCommend
        {
            get;
            set;
        }

        [JsonProperty("count_support")]
        public int CountSupport
        {
            get;
            set;
        }

        [JsonProperty("count_tread")]
        public int CountTread
        {
            get;
            set;
        }

        [JsonProperty("client")]
        public int Client
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

        [JsonProperty("usr")]
        public CommentUser User
        {
            get;
            set;
        }
    }
}
