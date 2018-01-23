using Microsoft.Toolkit.Uwp.Notifications;

using Windows.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.ExtendedExecution;

namespace MyCutApp
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        bool _isInBackgroundMode = false;
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            this.EnteredBackground += App_EnteredBackground;
            this.LeavingBackground += App_LeavingBackground;

        }

        private void App_EnteredBackground(object sender,EnteredBackgroundEventArgs e)
        {
            if (!_isInBackgroundMode)
            {
                ShowToast("进入后台任务", "陈初静去休息一会");
            }

            _isInBackgroundMode = true;
           
        }

        private void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            if (_isInBackgroundMode)
            {
                ShowToast("离开后台任务", "我陈初静又回来了");
            }
            _isInBackgroundMode = false;
            
        }

        public void ShowToast(string msg, string subMsg = null)
        {
            var toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
            {
                new AdaptiveText()
                {
                    Text = msg
                },
                new AdaptiveText()
                {
                    Text =subMsg
                }
            },
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "ms-appx:///Assets/molome.jpg",
                            HintCrop = ToastGenericAppLogoCrop.Circle
                        }
                    }
                },
                Actions = new ToastActionsCustom(),
                Launch = "action=viewFriendRequest&userId=49183"
            };

            // Create the toast notification
            var toastNotif = new ToastNotification(toastContent.GetXml());

            // And send the notification
            ToastNotificationManager.CreateToastNotifier().Show(toastNotif);
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override async  void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();

                Window.Current.Content = rootFrame;

               
                
            }

            var session = new ExtendedExecutionSession
            {
                Reason = ExtendedExecutionReason.Unspecified
            };
            session.Revoked += Session_Revoked;
            ExtendedExecutionResult result = await session.RequestExtensionAsync();
        }

        private void Session_Revoked(object sender, ExtendedExecutionRevokedEventArgs args)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }
    }
}
