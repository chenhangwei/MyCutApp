using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyCutApp.SetPages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SetToolPage : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        Setting setting = new Setting();
        public static string PageName { get; set; } = "ToolSetData";
        public static string TitleName { get; set; } = "一般工具设置";
  
        public SetToolPage()
        {
            this.InitializeComponent();
            VoiceSwitch.IsOn = setting.ReadSettings<bool>("VoiceSwitch", false);
            MusicSwitch.IsOn= setting.ReadSettings<bool>("MusicSwitch", false);
        }
        private void VoiceSwitch_Changed(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle.IsOn)
            {
                localSettings.Values["VoiceSwitch"] = true;
                
            }
            else
            {
                localSettings.Values["VoiceSwitch"] = false;
            }
          

        }

        private void MusicSwitch_Changed(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            if (toggle.IsOn)
            {
                localSettings.Values["MusicSwitch"] = true;

            }
            else
            {
                localSettings.Values["MusicSwitch"] = false;
            }
           
        }

        private void NOSwitch_Changed(object sender, RoutedEventArgs e)
        {

        }
     
    }
}
