using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.DataSources
{
    public class CommentSource : IncrementalItemSourceBase<Comment>
    {
        private readonly Feed _feed;

        private readonly ICommentService _service;

        private int _currentPage = 0;

        public CommentSource(ICommentService service, Feed feed)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            this._service = service;
            this._feed = feed;
        }

        protected override async Task LoadMoreItemsAsync(ICollection<Comment> collection, uint suggestLoadCount)
        {
            try
            {
                // 读取下一页数据。
                int nextPage = this._currentPage + 1;
                DataResultBase<ResultList<Comment>> result = await this._service.GetCommentsAsync(this._feed, nextPage);
                if (result.Code == 0)// 请求成功。
                {
                    ResultList<Comment> resultList = result.Data;
                    Comment[] comments = resultList.Data;
                    foreach (Comment comment in comments)
                    {
                        // 添加不重复的数据。
                        if (collection.Any(temp => temp.Id == comment.Id) == false)
                        {
                            collection.Add(comment);
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

        protected override void OnRefresh(ICollection<Comment> collection)
        {
            this._currentPage = 0;
        }
    }
}