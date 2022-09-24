using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;

namespace SleepWell.Droid
{
    [Activity(Label = "SleepWell", MainLauncher = true, Theme ="@style/MyTheme.Splash", NoHistory = true)]
    public class SleepWell : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        protected override async void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }

        async Task SimulateStartup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}