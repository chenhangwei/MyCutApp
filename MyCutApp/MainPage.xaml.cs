using MyCutApp.Model;
using MyCutApp.SetPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace MyCutApp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        string MessageFontColoer { get; set; } = "black";
        Setting setting = new Setting();
        DispatcherTimer timer;//定义定时器
        public string DateTimer { get; set; }
        public Trajectory Xyr { get; set; } = new Trajectory(0, 0, 0, 0, 100);
        public string Warning { get; set; } = "Now playing...";
        CutMana mana = new CutMana();
        ComboBoxItem lbi = new ComboBoxItem();
        public int AnyV { get; set; }
        
        MediaPlayer player { get; set; } = new MediaPlayer();
        


        public MainPage()
        {


            this.InitializeComponent();

            var mediaSource = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/musice.mp3"));
            player.Source = mediaSource;
            RunMedia.SetMediaPlayer(player);

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 50)
            };
            timer.Tick += Timer_Tick;//每秒触发这个事件，以刷新指针
            timer.Start();


        }

        private void Timer_Tick(object sender, object e)
        {
            DateTimer = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ");
            Bindings.Update();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Rotation();

        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                string str = "打开文件";
                Warning = str;
                setting.StopMusicasync(str);

                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                openPicker.FileTypeFilter.Add(".nc");
                openPicker.FileTypeFilter.Add(".txt");
                openPicker.FileTypeFilter.Add(".iso");
                openPicker.FileTypeFilter.Add(".dxf");
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    // Application now has read/write access to the picked file
                    Warning = "Picked photo: " + file.Name;
                   
                }
                else
                {
                    Warning = "Operation cancelled.";
                }
                try
                {
                 Stream streamfile = await file.OpenStreamForReadAsync();
                
                StreamReader dxfFilm = new StreamReader(streamfile);

              await ReadDxfFilm(dxfFilm);

                dxfFilm.Dispose();
                streamfile.Dispose();
                }
                catch (Exception)
                {

                   
                }
             
               


                //FolderPicker.pickSingleFolderAsync().then(function(folder) {
                //    if (folder)
                //    {
                //        // Process picked folder

                //        // Store folder for future access
                //        var folderToken = Windows.Storage.AccessCache.StorageApplicationPermissions.futureAccessList.add(folder);
                //    }
                //    else
                //    {
                //        // The user didn't pick a folder
                //    }
                //});



            }
            else
            {


            }

        }

        private async Task<bool> ReadDxfFilm(StreamReader streamReader)
        {
            List<string> lists = new List<string>();

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile =await storageFolder.CreateFileAsync("dxf.txt", CreationCollisionOption.ReplaceExisting);
            Stream stream =await storageFile.OpenStreamForWriteAsync();
            StreamWriter streamWriter = new StreamWriter(stream);



            while (streamReader.ReadLine()!=null)
            {
               var filmstr =streamReader.ReadLine();
               

                lists.Add (filmstr);
            }

            foreach (var item in lists)
            {
               
                 streamWriter.WriteLine(item);
            }
            streamWriter.Dispose();
            stream.Dispose();
            return true;
        }

        private void GoToStart_Click(object sender, RoutedEventArgs e)
        {
            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                string str = "模拟路径";
                Warning = str;
                setting.StopMusicasync(str);
            }
            else
            {

            }

        }

        private void AddSpeed_Click(object sender, RoutedEventArgs e)
        {

            if (Xyr.VelocityRatio < 96)
            {
                string str = "加速";
                setting.StopMusicasync(str);
                ushort a = 5;
                Xyr.VelocityRatio = (ushort)(Xyr.VelocityRatio + a);
                Bindings.Update();
            }
            else
            {
                return;
            }
        }

        private void PYCut_Click(object sender, RoutedEventArgs e)
        {

            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                if (AnyV == 0)
                {
                    string str = "Y正方向运动";
                    Warning = str;
                    setting.StopMusicasync(str);
                }
                else
                {
                    string str = "Y正运动" + AnyV + "毫米";
                    Warning = str;
                    setting.StopMusicasync(str);

                    Xyr.Py += AnyV;
                }

            }


        }

        private void SubSpeed_Click(object sender, RoutedEventArgs e)
        {

            if (Xyr.VelocityRatio > 5)
            {
                string str = "减速";
                setting.StopMusicasync(str);
                ushort a = 5;
                Xyr.VelocityRatio = (ushort)(Xyr.VelocityRatio - a);
                Bindings.Update();
            }
            else
            {
                return;
            }
        }

        private void NXCut_Click(object sender, RoutedEventArgs e)
        {


            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                if (AnyV == 0)
                {
                    string str = "X负方向运动";
                    Warning = str;
                    setting.StopMusicasync(str);
                }
                else
                {
                    string str = "X负运动" + AnyV + "毫米";
                    Warning = str;
                    setting.StopMusicasync(str);

                    Xyr.Px -= AnyV;
                }
            }
            Bindings.Update();

        }

        private void SetLength_Changed(object sender, SelectionChangedEventArgs e)
        {

            int a = (int)SetLength.SelectedValue;
            AnyV = a;
        }

        private void PXCut_Click(object sender, RoutedEventArgs e)
        {

            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                if (AnyV == 0)
                {
                    string str = "X正方向运动";
                    Warning = str;
                    setting.StopMusicasync(str);
                }
                else
                {
                    string str = "X正运动" + AnyV + "毫米";
                    Warning = str;
                    setting.StopMusicasync(str);
                    Xyr.Px += AnyV;
                }
            }
            Bindings.Update();

        }

        private void LfetTurn_Click(object sender, RoutedEventArgs e)
        {
            if (Run.Content.ToString() == "运行程序")
            {
                string str = "刀具左旋";
                setting.StopMusicasync(str);
            }

        }

        private void NYCut_Click(object sender, RoutedEventArgs e)
        {

            if (Run.Content.ToString() == "运行程序")
            {
                MessageFontColoer = "Black";
                if (AnyV == 0)
                {
                    string str = "Y负方向运动";
                    Warning = str;
                    setting.StopMusicasync(str);
                }
                else
                {
                    string str = "Y负运动" + AnyV + "毫米";
                    Warning = str;
                    setting.StopMusicasync(str);

                    Xyr.Py -= AnyV;
                }

            }


        }

        private void RightTurn_Click(object sender, RoutedEventArgs e)
        {
            if (Run.Content.ToString() == "运行程序")
            {
                string str = "刀具右旋";
                setting.StopMusicasync(str);
            }

        }

        private void ResetMachine_Click(object sender, RoutedEventArgs e)
        {

            if (Run.Content.ToString() == "运行程序")
            {
                string str = "程序复位";
                MessageFontColoer = "lightSeaGreen";
                Warning = "复位中...";
                Xyr = new Trajectory(0, 0, 0, 0, 100);
                setting.StopMusicasync(str);
              


            }
            else
            {
                MessageFontColoer = "lightSeaGreen";
                Warning = "程序运行中无法复位";

            }

        }

        private void RunProgram_Click(object sender, RoutedEventArgs e)
        {

            CutMana.Dd = true;
            if (Run.Content.ToString() == "运行程序" || Run.Content.ToString() == "继续运行")
            {
                string str = "程序运行";
                setting.StopMusicasync(str);
                RunRing.IsActive = true;

                bool b = setting.ReadSettings<bool>("MusicSwitch", false);
                if (b)
                {
                    player.Play();
                }
            
             

                try {
                    MyGrid.Children.Remove(mana.DrawCut(Xyr));
                    MyGrid.Children.Add(mana.DrawCut(Xyr));
                }
                catch (Exception)
                {

                }

                Run.Content = "暂停";
                MessageFontColoer = "Green";


                mana.CutAdd(Xyr);
                Warning = CutMana.Runstr;
            }
            else
            {
                Run.Content = "继续运行";

                Warning = "程序暂停中...";
                RunRing.IsActive = false;
                player.Pause();
                CutMana.Dd = false;

            }

        }

        private void StopPgrogram_Click(object sender, RoutedEventArgs e)
        {
            string str = "程序停止";
            RunRing.IsActive = false;
            Run.Content = "运行程序";
            MessageFontColoer = "Red";
            Warning = "程序已停止";
            CutMana.Dd = false;
            setting.StopMusicasync(str);
            player.Pause();
            MyGrid.Children.Clear();

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            string str = "设置";
            setting.StopMusicasync(str);
            Frame.Navigate(typeof(Setting));
        }

        private void Page_Load(object sender, RoutedEventArgs e)
        {
            
        }

        private void Rotation()
        {

        
            MyButton.RenderTransformOrigin = new Point(0.5, 0.5);

            CompositeTransform c = new CompositeTransform();
           
            MyButton.RenderTransform =c;
           
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation1 = new DoubleAnimation
            {
                
                From = 0,
                To = 200,
                Duration = new Duration(TimeSpan.FromSeconds(20)),
            };
            DoubleAnimation animation2 = new DoubleAnimation
            {

                From = 0,
                To = -200,
                Duration = new Duration(TimeSpan.FromSeconds(20)),
            };
            DoubleAnimation animation3 = new DoubleAnimation
            {

                From = 0,
                To = 720,
                Duration = new Duration(TimeSpan.FromSeconds(20)),
            };

            Storyboard.SetTarget(animation1, MyButton);
            Storyboard.SetTarget(animation2, MyButton);
            Storyboard.SetTarget(animation3, MyButton);
            Storyboard.SetTargetName(animation1, "button");
            Storyboard.SetTargetProperty(animation1, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
            Storyboard.SetTargetProperty(animation2, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            Storyboard.SetTargetProperty(animation3, "(UIElement.RenderTransform).(CompositeTransform.Rotation)");

            storyboard.Children.Add(animation1);
            storyboard.Children.Add(animation2);
            storyboard.Children.Add(animation3);
            storyboard.Begin();


        }

        private void MyScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            
            
            MyScrollViewer.ChangeView(MyScrollViewer.ScrollableWidth/2 , MyScrollViewer.ScrollableHeight/2 , 1, false);

        }

        private  void Forward_Click(object sender, RoutedEventArgs e)
        {


          
        }

      
    }

}



