using SoftwareKobo.UniversalToolkit.Controls;
using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class FeedView : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(FeedView), new PropertyMetadata(null, ItemsSourceChanged));

        public FeedView()
        {
            this.InitializeComponent();
            this.Loaded += this.FeedView_Loaded;
            this.Unloaded += this.FeedView_Unloaded;
        }

        public event ItemClickEventHandler ItemClick;

        public event EventHandler ScrollDown;

        public event EventHandler ScrollUp;

        public object ItemsSource
        {
            get
            {
                return this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FeedView obj = (FeedView)d;
            object value = e.NewValue;

            obj.view.ItemsSource = value;
        }

        private void AdaptiveCollectionView_ScrollDown(object sender, EventArgs e)
        {
            if (this.ScrollDown != null)
            {
                this.ScrollDown(this, e);
            }
        }

        private void AdaptiveCollectionView_ScrollUp(object sender, EventArgs e)
        {
            if (this.ScrollUp != null)
            {
                this.ScrollUp(this, e);
            }
        }

        private void FeedView_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += this.Window_SizeChanged;
            this.ResetMode();
        }

        private void FeedView_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= this.Window_SizeChanged;
        }

        private void ResetMode()
        {
            Rect size = Window.Current.Bounds;
            double width = size.Width;
            if (width < 548)
            {
                this.view.Mode = AdaptiveCollectionViewMode.List;
            }
            else if (width >= 548 && width < 720)
            {
                this.view.Mode = AdaptiveCollectionViewMode.List;
            }
            else if (width >= 720 && width < 900)
            {
                this.view.Mode = AdaptiveCollectionViewMode.Grid;
            }
            else
            {
                this.view.Mode = AdaptiveCollectionViewMode.Grid;
            }
        }

        private void View_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ItemClick != null)
            {
                this.ItemClick(sender, e);
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.ResetMode();
        }
    }
}