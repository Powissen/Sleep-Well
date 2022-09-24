using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.LocalNotification;
using System.IO;

namespace SleepWell.Droid
{
    [Activity(Label = "Sleep_well", Icon = "@drawable/SleepWellLogo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "dat.txt");
            base.OnCreate(savedInstanceState);

            NotificationCenter.CreateNotificationChannel();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //if (!File.Exists(_filePath))
            //{
            //    AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            //    alertDialog.SetTitle("Warning");
            //    alertDialog.SetMessage("Pre správne fungovanie aplikácie je potrebné zakázať optimalizáciu batérie pre túto aplikáciu! \n\nBattery optimization for this app must be disabled for the app to work properly!");
            //    alertDialog.SetNeutralButton("OPEN SETTINGS", delegate {
            //        alertDialog.Dispose();
            //        Intent intent = new Intent();
            //        intent.SetAction(Android.Provider.Settings.ActionIgnoreBatteryOptimizationSettings);
            //        Forms.Context.StartActivity(intent);
            //    });
            //    alertDialog.Show();
            //}

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}