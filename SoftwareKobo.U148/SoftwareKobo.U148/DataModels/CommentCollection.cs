using SoftwareKobo.U148.DataSources;
using SoftwareKobo.U148.Models;
using SoftwareKobo.U148.Services;
using SoftwareKobo.UniversalToolkit.Mvvm;

namespace SoftwareKobo.U148.DataModels
{
    public class CommentCollection : IncrementalLoadingCollection<Comment, CommentSource>
    {
        public CommentCollection(ICommentService service, Feed feed) : base(new CommentSource(service, feed))
        {
        }
    }
}