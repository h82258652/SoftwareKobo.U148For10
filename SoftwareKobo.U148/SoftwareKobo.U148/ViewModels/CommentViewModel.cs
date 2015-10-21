using System.Runtime.CompilerServices;
using SoftwareKobo.U148.DataModels;
using SoftwareKobo.UniversalToolkit.Mvvm;
using Windows.UI.Xaml;
using SoftwareKobo.U148.Models;

namespace SoftwareKobo.U148.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        private CommentCollection _comments;

        public CommentCollection Comments
        {
            get
            {
                return _comments;
            }
            private set
            {
                this.Set(ref this._comments, value);
            }
        }

        protected override void ReceiveFromView(FrameworkElement originSourceView, dynamic parameter)
        {
            if (parameter is Feed)
            {
                this.Comments = new CommentCollection(parameter);
            }
        }
    }
}