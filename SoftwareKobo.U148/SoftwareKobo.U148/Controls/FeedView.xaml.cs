using SoftwareKobo.UniversalToolkit.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class FeedView : AdaptiveCollectionView
    {
        public FeedView()
        {
            this.InitializeComponent();
            this.SizeChanged += (sender, e) =>
            {
                if (e.NewSize.Width >= 1280)
                {
                    this.Mode = AdaptiveCollectionViewMode.Grid;
                }
                else
                {
                    this.Mode = AdaptiveCollectionViewMode.List;
                }
            };
        }
    }
}