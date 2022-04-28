using System;
using System.Collections;
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
        Saving saving = new Saving();
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");


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


            saving.alarmEnabled = Convert.ToBoolean(File.ReadLines(_filePath).First());
            saving.darkMode = Convert.ToBoolean(File.ReadLines(_filePath).Last());
            //saving.alarmTime = DateTime.Parse(File.ReadLines(_filePath).Last());

            if (saving.alarmEnabled)
            {
                _switch.IsToggled = true;
            }

            if (saving.darkMode)
            {
                DarkModeCheckBox.IsChecked = true;
            }

        }

        void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                writer.WriteLine(saving.alarmEnabled);
                writer.WriteLine(saving.darkMode);
                //writer.WriteLine(_timePicker.Time);
            }
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
                saving.alarmEnabled = true;

                _triggerTime = DateTime.Today + _timePicker.Time;
                if (_triggerTime < DateTime.Now)
                {
                    _triggerTime += TimeSpan.FromDays(1);
                }
            }
            else
            {
                saving.alarmEnabled = false;
            }
            SaveData();
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
                saving.darkMode = true;
                SaveData();
            }
            else
            {
                BackgroundColor = Color.White;
                textColour = "Black";
                barColour = "DarkGray";
                OnPropertyChanged(nameof(textColour));
                OnPropertyChanged(nameof(barColour));
                saving.darkMode = false;
                SaveData();
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
