using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.DataSources
{
    public class SearchedFeedSource : IncrementalItemSourceBase<Feed>
    {
        private readonly string _keyword;

        private readonly ISearchService _service;

        private int _currentPage = 0;

        public SearchedFeedSource(ISearchService service, string keyword)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (keyword == null)
            {
                throw new ArgumentNullException(nameof(keyword));
            }
            if (keyword.Length <= 0)
            {
                throw new ArgumentException("keyword must be not empty.", nameof(keyword));
            }

            this._service = service;
            this._keyword = keyword;
        }

        protected override async Task LoadMoreItemsAsync(ICollection<Feed> collection, uint suggestLoadCount)
        {
            try
            {
                // 读取下一页数据。
                int nextPage = this._currentPage + 1;
                DataResultBase<ResultList<Feed>> result = await this._service.GetSearchResultsAsync(this._keyword, nextPage);
                if (result.Code == 0)// 请求成功。
                {
                    ResultList<Feed> resultList = result.Data;
                    Feed[] feeds = resultList.Data;
                    foreach (Feed feed in feeds)
                    {
                        // 添加不重复的数据。
                        if (collection.Any(temp => temp.Id == feed.Id) == false)
                        {
                            collection.Add(feed);
                        }
                    }

                    if (resultList.PageCount == this._currentPage)
                    {
                        // 已经到达最后一页。
                        this.RaiseHasMoreItemsChanged(false);
                    }
                    else
                    {
                        this._currentPage = nextPage;
                    }
                }
            }
            catch
            {
            }
        }

        protected override void OnRefresh(ICollection<Feed> collection)
        {
            this._currentPage = 0;
        }
    }
}