using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Controls
{
    // 此委托为了解决 Behaviors SDK 的 EventTriggerBehavior 异常。
    public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);
}