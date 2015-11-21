using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System;

namespace SoftwareKobo.U148.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly ISearchService _service;

        public SearchViewModel(ISearchService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this._service = service;
        }

        private FeedCollection _searchedResults;

        public FeedCollection SearchedResults
        {
            get
            {
                return _searchedResults;
            }
            set
            {
                this.Set(ref _searchedResults, value);
            }
        }

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand
        {
            get
            {
                if (this._searchCommand == null)
                {
                    this._searchCommand = new DelegateCommand<string>(query =>
                    {
                        SearchedResults = new FeedCollection(this._service, query);
                    });
                }
                return this._searchCommand;
            }
        }

        protected override void ReceiveFromView(dynamic parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrEmpty(query) == false)
            {
                this.SearchCommand.Execute(query);
            }
        }
    }
}