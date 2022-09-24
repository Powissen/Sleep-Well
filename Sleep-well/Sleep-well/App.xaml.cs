using Xamarin.Forms;


[assembly: ExportFont("SevenSegment.ttf", Alias = "SevenSegment")]
[assembly: ExportFont("HeaderFont.ttf", Alias = "HeaderFont")]
namespace SleepWell
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
