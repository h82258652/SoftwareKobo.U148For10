﻿using SoftwareKobo.U148.Models;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace SoftwareKobo.U148.Services
{
    public class SearchService : ISearchService
    {
        private const string SearchTemplate = "http://api.u148.net/json/search/{0}?keyword={1}";

        public Task<DataResultBase<ResultList<Feed>>> GetSearchResultsAsync(string keyword, int page = 1)
        {
            if (keyword == null)
            {
                throw new ArgumentNullException(nameof(keyword));
            }
            if (keyword.Length <= 0)
            {
                throw new ArgumentException("query could not be empty.", nameof(keyword));
            }
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should greater than zero.");
            }

            string url = string.Format(SearchTemplate, page, keyword);
            using (HttpClient client = new HttpClient())
            {
                return client.GetJsonAsync<DataResultBase<ResultList<Feed>>>(new Uri(url));
            }
        }
    }
}