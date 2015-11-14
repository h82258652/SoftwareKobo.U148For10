using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class UserService : IUserService
    {
        private const string LOGIN_TEMPLATE = @"http://api.u148.net/json/login";

        private const string GET_FAVOURITES_TEMPLATE = @"http://api.u148.net/json/get_favourite/0/{0}?token={1}";

        private const string ADD_FAVOURITE_TEMPLATE = @"http://api.u148.net/json/favourite?id={0}&token={1}";

        public async Task<DataResultBase<UserInfo>> LoginAsync(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (email.Length <= 0)
            {
                throw new ArgumentException("email could not be empty.", nameof(email));
            }
            if (password.Length <= 0)
            {
                throw new ArgumentException("email could not be empty.", nameof(email));
            }

            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("email", email);
            postData.Add("password", password);

            string json;
            using (HttpClient client = new HttpClient())
            {
                using (IHttpContent httpContent = new HttpFormUrlEncodedContent(postData))
                {
                    HttpResponseMessage response = await client.PostAsync(new Uri(LOGIN_TEMPLATE), httpContent);
                    json = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<DataResultBase<UserInfo>>(json);
        }

        public async Task<ResultBase> AddFavouriteAsync(UserInfo userInfo, Feed feed)
        {
            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            string url = string.Format(ADD_FAVOURITE_TEMPLATE, feed.Id, userInfo.Token);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<ResultBase>(new Uri(url));
            }
        }

        public async Task<DataResultBase<ResultList<Favourite>>> GetFavouritesAsync(UserInfo userInfo, int page = 1)
        {
            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            string url = string.Format(GET_FAVOURITES_TEMPLATE, page, userInfo.Token);
            using (HttpClient client = new HttpClient())
            {
                return await client.GetJsonAsync<DataResultBase<ResultList<Favourite>>>(new Uri(url));
            }
        }
    }
}