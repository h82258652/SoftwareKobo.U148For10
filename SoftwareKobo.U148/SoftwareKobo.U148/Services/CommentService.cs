using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class CommentService : ICommentService
    {
        private const string GetCommentTemplate = @"http://api.u148.net/json/get_comment/{0}/{1}";

        private const string SendCommentTemplate = @"http://api.u148.net/json/comment";

        public async Task<DataResultBase<ResultList<Comment>>> GetCommentsAsync(Feed feed, int page = 1)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should greater than zero.");
            }

            string url = string.Format(GetCommentTemplate, feed.Id, page);
            url = url + "?t=" + DateTime.Now.Ticks;
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<DataResultBase<ResultList<Comment>>>(new Uri(url));
            }
        }

        public async Task<ResultBase> SendCommentAsync(Feed feed, UserInfo userInfo, string content, SimulateDevice device = SimulateDevice.Android, Comment reviewComment = null)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }
            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            if (content.Length <= 0)
            {
                throw new ArgumentException("评论不能为空", nameof(content));
            }
            if (Enum.IsDefined(typeof(SimulateDevice), device) == false)
            {
                throw new ArgumentException("模拟设备未定义。", nameof(device));
            }

            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                {
                    "id", feed.Id.ToString()
                },
                {
                    "token", userInfo.Token
                }
            };
            switch (device)
            {
                case SimulateDevice.Android:
                    postData.Add("client", "android");
                    break;

                case SimulateDevice.IPhone:
                    postData.Add("client", "iphone");
                    break;
            }
            postData.Add("content", content);
            if (reviewComment != null)
            {
                postData.Add("review_id", reviewComment.Id.ToString());
            }

            string json;
            using (HttpClient client = new HttpClient())
            {
                using (IHttpContent httpContent = new HttpFormUrlEncodedContent(postData))
                {
                    HttpResponseMessage response = await client.PostAsync(new Uri(SendCommentTemplate), httpContent);
                    json = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<ResultBase>(json);
        }
    }
}