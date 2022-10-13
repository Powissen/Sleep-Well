using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using SleepWell.Droid;
using SleepWell.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ForegroundService))]

namespace SleepWell.Droid
{
    [Service]
    public class ForegroundService : Service, IForegroundService
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            string channelID = "ForegroundServiceChannel";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var notfificationChannel = new NotificationChannel(channelID, channelID, NotificationImportance.Low);
            notificationManager.CreateNotificationChannel(notfificationChannel);
            var notificationBuilder = new NotificationCompat.Builder(this, channelID)
                     .SetContentTitle("Good night!")
                     .SetSmallIcon(Resource.Drawable.SleepWellLogo)
                     .SetContentText("Your alarm is setted up and turned on.")
                     .SetPriority(1)
                     .SetOngoing(true)
                     .SetChannelId(channelID)
                     .SetAutoCancel(true);

            StartForeground(1001, notificationBuilder.Build());

            return base.OnStartCommand(intent, flags, startId);
        }


        public void OpenAlarmClock()
        {
            if (StartAppIfInstalled("com.android.deskclock")){}
            else if (StartAppIfInstalled("com.google.android.deskclock")){}
            else OpenPlayStore();
        }
        public bool StartAppIfInstalled(string packageName)
        {
            try
            {
                Intent intent2 = Android.App.Application.Context.PackageManager.GetLaunchIntentForPackage(packageName);
                intent2.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent2);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async void OpenPlayStore()
        {
            string url = "https://play.google.com/store/apps/details?id=com.google.android.deskclock";
            await Browser.OpenAsync(url, BrowserLaunchMode.External);
        }


        public void StartMyForegroundService()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
                Android.App.Application.Context.StartForegroundService(intent);
            }
        }
        public void StopMyForegroundService()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
                Android.App.Application.Context.StopService(intent);
            }
        }
    }
}