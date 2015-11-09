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
        private const string GET_COMMENT_TEMPLATE = @"http://api.u148.net/json/get_comment/{0}/{1}";

        private const string SEND_COMMENT_TEMPLATE = @"http://api.u148.net/json/comment";

        public async Task<ResultBase<ResultList<Comment>>> GetCommentsAsync(Feed feed, int page = 1)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should greater than zero.");
            }

            string url = string.Format(GET_COMMENT_TEMPLATE, feed.Id, page);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<ResultBase<ResultList<Comment>>>(new Uri(url));
            }
        }

        public async Task<SendCommentResult> SendCommentAsync(Feed feed, UserInfo userInfo, string content, SimulateDevice device = SimulateDevice.Android, Comment reviewComment = null)
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

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("id", feed.Id.ToString());
            postData.Add("token", userInfo.Token);
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
                    HttpResponseMessage response = await client.PostAsync(new Uri(SEND_COMMENT_TEMPLATE), httpContent);
                    json = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<SendCommentResult>(json);
        }
    }
}