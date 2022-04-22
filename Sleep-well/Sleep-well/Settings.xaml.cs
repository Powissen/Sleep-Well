using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Android.Media;

namespace SleepWell
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Settings : ContentPage
    {
        public string textColour { get; set; } = "Black";
        public string barColour { get; set; } = "LightGray";

        DateTime _triggerTime;
        //public string music = "Nokia Ringtone Arabic 1 HOur.mp3";
        //protected MediaPlayer player;
        //public void StartPlayer(String filePath)
        //{
        //    if (player == null)
        //    {
        //        player = new MediaPlayer();
        //    }
        //    else
        //    {
        //        player.Reset();
        //        player.SetDataSource(filePath);
        //        player.Prepare();
        //        player.Start();
        //    }
        //}
        public Settings()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

            BindingContext = this;

            LanguagePicker.Items.Add("Slovenčina");
            LanguagePicker.Items.Add("English");
            LanguagePicker.SelectedIndex = 0;
        }

        void OpenMainPage(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }






        bool OnTimerTick()
        {
            if (_switch.IsToggled && DateTime.Now >= _triggerTime)
            {
                _switch.IsToggled = false;
                DisplayAlert("Timer Alert", "The '" + _entry.Text + "' timer has elapsed", "OK");
                //StartPlayer("ringtone.mp3");
            }
            return true;
        }

        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                SetTriggerTime();
            }
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            SetTriggerTime();
        }

        void SetTriggerTime()
        {
            if (_switch.IsToggled)
            {
                _triggerTime = DateTime.Today + _timePicker.Time;
                if (_triggerTime < DateTime.Now)
                {
                    _triggerTime += TimeSpan.FromDays(1);
                }
            }
        }

        private void CheckBox_DarkMode(object sender, CheckedChangedEventArgs e)
        {

            if (DarkModeCheckBox.IsChecked)
            {
                BackgroundColor = Color.FromHex("1f1f1f");
                textColour = "DarkGray";
                barColour = "Black";
                OnPropertyChanged(nameof(textColour));
                OnPropertyChanged(nameof(barColour));
            }
            else
            {
                BackgroundColor = Color.White;
                textColour = "Black";
                barColour = "DarkGray";
                OnPropertyChanged(nameof(textColour));
                OnPropertyChanged(nameof(barColour));
            }
        }
        
        //Link na multilanguage:  https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/text?pivots=windows 

        private void LanguageChange(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedIndex == 0)
            {
                //SK
            }
            else
            {
                //EN
            }
        }
    }
}
