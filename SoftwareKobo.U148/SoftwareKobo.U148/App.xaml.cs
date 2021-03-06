﻿using SoftwareKobo.U148.Datas;
using SoftwareKobo.U148.Views;
using SoftwareKobo.UniversalToolkit;
using SoftwareKobo.UniversalToolkit.Extensions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UmengSDK;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SoftwareKobo.U148
{
    sealed partial class App : Bootstrapper
    {
        private const string UmengAppKey = @"57238b5de0f55af993003119";

        public App()
        {
            InitializeComponent();
            RequestedTheme = AppSettings.Instance.ThemeMode == ElementTheme.Dark ? ApplicationTheme.Dark : ApplicationTheme.Light;

            DefaultNavigatePage = typeof(MainView);

#if DEBUG
            DebugSettings.EnableFrameRateCounter = true;
            DebugSettings.EnableDisplayMemoryUsage();

            // 调试广告。
            AppSettings.Instance.LastClickAdTime = DateTime.MinValue;
#endif
        }

        protected override async Task OnPreStartAsync(IActivatedEventArgs args, AppStartInfo info)
        {
            await UmengAnalytics.StartTrackAsync(UmengAppKey);

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }

            if (RootFrame != null)
            {
                info.NavigatePage = null;
            }

#if DEBUG
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += delegate
            {
                var rect = Window.Current.Bounds;
                Debug.WriteLine(rect.Width + ":" + (rect.Height + 32));
            };
            timer.Start();
#endif
        }

        protected override async Task OnSuspendingAsync(object sender, SuspendingEventArgs e)
        {
            await UmengAnalytics.EndTrackAsync();
        }
    }

    //    /// <summary>
    //    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    //    /// </summary>
    //    sealed partial class App : Application
    //    {
    //        /// <summary>
    //        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
    //        /// 已执行，逻辑上等同于 main() 或 WinMain()。
    //        /// </summary>
    //        public App()
    //        {
    //            this.InitializeComponent();
    //            this.Suspending += OnSuspending;
    //        }

    //        /// <summary>
    //        /// 在应用程序由最终用户正常启动时进行调用。
    //        /// 将在启动应用程序以打开特定文件等情况下使用。
    //        /// </summary>
    //        /// <param name="e">有关启动请求和过程的详细信息。</param>
    //        protected override void OnLaunched(LaunchActivatedEventArgs e)
    //        {
    //#if DEBUG
    //            if (System.Diagnostics.Debugger.IsAttached)
    //            {
    //                this.DebugSettings.EnableFrameRateCounter = true;
    //            }
    //#endif

    //            Frame rootFrame = Window.Current.Content as Frame;

    //            // 不要在窗口已包含内容时重复应用程序初始化，
    //            // 只需确保窗口处于活动状态
    //            if (rootFrame == null)
    //            {
    //                // 创建要充当导航上下文的框架，并导航到第一页
    //                rootFrame = new Frame();

    //                rootFrame.NavigationFailed += OnNavigationFailed;

    //                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
    //                {
    //                    //TODO: 从之前挂起的应用程序加载状态
    //                }

    //                // 将框架放在当前窗口中
    //                Window.Current.Content = rootFrame;
    //            }

    //            if (rootFrame.Content == null)
    //            {
    //                // 当导航堆栈尚未还原时，导航到第一页，
    //                // 并通过将所需信息作为导航参数传入来配置
    //                // 参数
    //                rootFrame.Navigate(typeof(MainView), e.Arguments);
    //            }
    //            // 确保当前窗口处于活动状态
    //            Window.Current.Activate();
    //        }

    //        /// <summary>
    //        /// 导航到特定页失败时调用
    //        /// </summary>
    //        ///<param name="sender">导航失败的框架</param>
    //        ///<param name="e">有关导航失败的详细信息</param>
    //        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    //        {
    //            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    //        }

    //        /// <summary>
    //        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
    //        /// 无需知道应用程序会被终止还是会恢复，
    //        /// 并让内存内容保持不变。
    //        /// </summary>
    //        /// <param name="sender">挂起的请求的源。</param>
    //        /// <param name="e">有关挂起请求的详细信息。</param>
    //        private void OnSuspending(object sender, SuspendingEventArgs e)
    //        {
    //            var deferral = e.SuspendingOperation.GetDeferral();
    //            //TODO: 保存应用程序状态并停止任何后台活动
    //            deferral.Complete();
    //        }
    //    }
}