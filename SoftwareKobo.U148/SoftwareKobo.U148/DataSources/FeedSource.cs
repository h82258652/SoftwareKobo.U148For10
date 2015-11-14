using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.DataSources
{
    public class FeedSource : IncrementalItemSourceBase<Feed>
    {
        private readonly FeedCategory _category;

        private readonly IFeedService _service;

        private int _currentPage = 0;

        public FeedSource(IFeedService service, FeedCategory category)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (Enum.IsDefined(typeof(FeedCategory), category) == false)
            {
                throw new ArgumentException("feed category is not defined.", nameof(category));
            }

            this._service = service;
            this._category = category;
        }

        protected override async Task LoadMoreItemsAsync(ICollection<Feed> collection, uint suggestLoadCount)
        {
            try
            {
                // 读取下一页数据。
                int nextPage = this._currentPage + 1;
                DataResultBase<ResultList<Feed>> result = await this._service.GetFeedListAsync(this._category, nextPage);
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