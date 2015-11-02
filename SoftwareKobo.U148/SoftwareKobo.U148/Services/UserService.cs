using Newtonsoft.Json;
using SoftwareKobo.U148.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class UserService : IUserService
    {
        private const string LOGIN_TEMPLATE = @"http://api.u148.net/json/login";

        public async Task<ResultBase<UserInfo>> LoginAsync(string email, string password)
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
                using (IHttpContent httpContent = new HttpFormUrlEncodedContent(null))
                {
                    HttpResponseMessage response = await client.PostAsync(new Uri(LOGIN_TEMPLATE), httpContent);
                    json = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<ResultBase<UserInfo>>(json);
        }
    }
}