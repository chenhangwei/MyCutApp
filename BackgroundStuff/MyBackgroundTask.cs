using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.UI.Popups;

namespace BackgroundStuff
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            SendToast("Hi this is background Task");
        }

        private void SendToast(string v)
        {
            Debug.Write("===========Fark the farking farkers=============");

            
        }
    }
}
