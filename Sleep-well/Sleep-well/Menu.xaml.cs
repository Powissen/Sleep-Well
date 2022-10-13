using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepWell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        Saving saving = new Saving();
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");

        public Menu()
        {
            InitializeComponent();
            BindingContext = this;

            StreamReader sr = new StreamReader(_filePath);
            saving.darkMode = Convert.ToBoolean(sr.ReadLine());
            saving.alarmTime = DateTime.Parse(sr.ReadLine());
            saving.language = Convert.ToInt32(sr.ReadLine());
            saving.alarmNote = sr.ReadLine();
            saving.alarmSound = Convert.ToInt32(sr.ReadLine());
            saving.fallAsleepTime = Convert.ToInt32(sr.ReadLine());
            saving.musicToSleep = Convert.ToBoolean(sr.ReadLine());
            saving.builtinTimer = Convert.ToBoolean(sr.ReadLine());
            sr.Close();

            if (saving.language == 1)
            {
                SettingsLabel.Text = "Settings";
                StatsLabel.Text = "Sleep stats";
            }

        }

        public void OpenMainPage(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }

        public void OpenSettings(object sender, EventArgs args)
        {
            App.Current.MainPage = new Settings();
        }

        public void OpenStats(object sender, EventArgs args)
        {
            if (saving.language == 1)
            {
                DisplayAlert("Work in progress \n\n", "This feature will be avaible soon.", "OK");
            }
            else
            {
                DisplayAlert("Tu sa ešte pracuje \n\n", "Táto funkcia bude dostupná čoskoro.", "OK");
            }
        }
    }
}