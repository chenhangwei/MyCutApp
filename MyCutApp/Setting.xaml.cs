using MyCutApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using MyCutApp.SetPages;
using Windows.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyCutApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Setting : Page
    {


        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        SysSet Setsys { set; get; } = new SysSet();
        public string Titlestr { set; get; } = "设置页面";
        public Setting()
        {

            this.InitializeComponent();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            string str = "回首页";
            StopMusicasync(str);
            Frame.Navigate(typeof(MainPage));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            string str = "保存数据";
            StopMusicasync(str);
        }

        private void Humbar_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Setlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            var a = lbi.Name.ToString();
            string b = SetIOPage.PageName;
            string c = SetSysPage.PageName;
            string d = SetToolPage.PageName;
            string s = AboutPage.PageName;

            if (a == b)
            {
                Titlestr = SetIOPage.TitleName;

                MySetPageFrame.Navigate(typeof(SetIOPage));
            }
            else if (a == c)
            {
                Titlestr = SetSysPage.TitleName;
                MySetPageFrame.Navigate(typeof(SetSysPage));
            }
            else if (a == d)
            {
                Titlestr = SetToolPage.TitleName;
                MySetPageFrame.Navigate(typeof(SetToolPage));
            }
            else if (a == s)
            {
                Titlestr = AboutPage.TitleName;
                MySetPageFrame.Navigate(typeof(AboutPage));

            }

            StopMusicasync("选择" + Titlestr);
            Bindings.Update();

        }


        public T ReadSettings<T>(string key, T defaultValue)
        {

            if (localSettings.Values.ContainsKey(key))
            {
                return (T)localSettings.Values[key];
            }
            if (null != defaultValue)
            {
                return defaultValue;
            }
            return default(T);
        }

        public async void StopMusicasync(string str)
        {
            bool b = ReadSettings<bool>("VoiceSwitch", false);
            if (b)
            {
                MediaElement mediaElement = new MediaElement();
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(str);
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

        }

    }
}
