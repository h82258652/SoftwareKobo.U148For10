using Newtonsoft.Json;

namespace SoftwareKobo.U148.Models
{
    [JsonObject]
    public class ResultList<T>
    {
        [JsonProperty("pageNo")]
        public int PageIndex
        {
            get;
            set;
        }

        [JsonProperty("pageSize")]
        public int PageSize
        {
            get;
            set;
        }

        [JsonProperty("pageMax")]
        public int PageCount
        {
            get;
            set;
        }

        [JsonProperty("total")]
        public int Total
        {
            get;
            set;
        }

        [JsonProperty("num")]
        public int Num
        {
            get;
            set;
        }

        [JsonProperty("start")]
        public int Start
        {
            get;
            set;
        }

        [JsonProperty("end")]
        public int End
        {
            get;
            set;
        }

        /// <summary>
        /// 上一页是第几页。
        /// </summary>
        [JsonProperty("pre")]
        public int PreviousPage
        {
            get;
            set;
        }

        /// <summary>
        /// 下一页是第几页。
        /// </summary>
        [JsonProperty("next")]
        public int NextPage
        {
            get;
            set;
        }

        [JsonProperty("data")]
        public T[] Data
        {
            get;
            set;
        }
    }
}