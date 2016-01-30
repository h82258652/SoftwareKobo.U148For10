using SoftwareKobo.UniversalToolkit.Controls;
using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Controls
{
    public sealed partial class FeedView
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(FeedView), new PropertyMetadata(null, ItemsSourceChanged));

        public FeedView()
        {
            InitializeComponent();
            Loaded += FeedView_Loaded;
            Unloaded += FeedView_Unloaded;
        }

        public event ItemClickEventHandler ItemClick;

        public event EventHandler ScrollDown;

        public event EventHandler ScrollUp;

        public object ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
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
            ScrollDown?.Invoke(this, e);
        }

        private void AdaptiveCollectionView_ScrollUp(object sender, EventArgs e)
        {
            ScrollUp?.Invoke(this, e);
        }

        private void FeedView_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            ResetMode();
        }

        private void FeedView_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void ResetMode()
        {
            Rect size = Window.Current.Bounds;
            double width = size.Width;
            if (width < 548)
            {
                view.Mode = AdaptiveCollectionViewMode.List;
            }
            else if (width >= 548 && width < 720)
            {
                view.Mode = AdaptiveCollectionViewMode.List;
            }
            else if (width >= 720 && width < 900)
            {
                view.Mode = AdaptiveCollectionViewMode.Grid;
            }
            else
            {
                view.Mode = AdaptiveCollectionViewMode.Grid;
            }
        }

        private void View_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemClick?.Invoke(sender, e);
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ResetMode();
        }
    }
}