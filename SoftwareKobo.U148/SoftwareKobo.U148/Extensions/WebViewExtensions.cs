using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace SoftwareKobo.U148.Extensions
{
    public static class WebViewExtensions
    {
        public static async Task<string> InvokeScriptAsync(this WebView webView, string scriptName, params string[] arguments)
        {
            return await webView.InvokeScriptAsync(scriptName, arguments);
        }

        public static Task WaitForDOMContentLoadedAsync(this WebView webView)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                webView.DOMContentLoaded -= handler;
                tcs.SetResult(null);
            };
            webView.DOMContentLoaded += handler;
            return tcs.Task;
        }
    }
}