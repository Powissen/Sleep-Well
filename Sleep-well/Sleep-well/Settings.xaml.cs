using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepWell
{

    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class Settings : ContentPage
    {
        public string textColour { get; set; } = "Black";
        public string barColour { get; set; } = "LightGray";

        DateTime _triggerTime;


        public Settings()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

            BindingContext = this;

            LanguagePicker.Items.Add("Slovenčina");
            LanguagePicker.Items.Add("English");
            LanguagePicker.SelectedIndex = 0;

            BeforeAlarmPicker.Items.Add("5min");
            BeforeAlarmPicker.Items.Add("10min");
            BeforeAlarmPicker.Items.Add("15min");
            BeforeAlarmPicker.Items.Add("20min");
            BeforeAlarmPicker.Items.Add("25min");
            BeforeAlarmPicker.Items.Add("30min");
            BeforeAlarmPicker.SelectedIndex = 0;


            //Toto je test ukladania - nevsimat ------------------------------------------
            Saving saving = new Saving();

            saving.test = "Toto je text co by sa mal ulozit";

            string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
            File.WriteAllText(_fileName, saving.ToString());
            //-----------------------------------------------------------------------------
        }

        void OpenMainPage(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }



       // ---------- Timer ----------
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

        // -------------------------------------------------------------------------------------------
        private void CheckBox_DarkMode(object sender, CheckedChangedEventArgs e)
        {

            if (DarkModeCheckBox.IsChecked)
            {
                BackgroundColor = Color.FromHex("1f1f1f");
                textColour = "LightGray";
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
                Header_Description.Text = "Nastavenia"; 
            }
            else
            {
                Header_Description.Text = "Settings";
            }
        }
        private void BeforeAlarmChange(object sender, EventArgs e)
        {
            //Zmena času povolenia zobudenia skôr
        }
    }
}
