using SoftwareKobo.U148.DataModels;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Extensions;
using SoftwareKobo.UniversalToolkit.Mvvm;
using System.Collections.Generic;

namespace SoftwareKobo.U148.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Dictionary<FeedCategory, FeedCollection> _categories = new Dictionary<FeedCategory, FeedCollection>();

        public MainViewModel()
        {
            foreach (FeedCategory category in EnumExtensions.GetValues<FeedCategory>())
            {
                _categories[category] = new FeedCollection(category);
            }
        }

        public Dictionary<FeedCategory, FeedCollection> Categories
        {
            get
            {
                return this._categories;
            }
        }

        private DelegateCommand _detailCommand;

        public DelegateCommand DetailCommand
        {
            get
            {
                return _detailCommand;
            }
        }
    }
}