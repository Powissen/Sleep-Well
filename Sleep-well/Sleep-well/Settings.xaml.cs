using System;
using System.ComponentModel;
using System.IO;
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

            BindingContext = this;

            LanguagePicker.Items.Add("Slovenčina");
            LanguagePicker.Items.Add("English");



            StreamReader sr = new StreamReader(_filePath);
            saving.darkMode = Convert.ToBoolean(sr.ReadLine());
            saving.alarmTime = DateTime.Parse(sr.ReadLine());
            saving.language = Convert.ToInt32(sr.ReadLine());
            saving.alarmNote = sr.ReadLine();
            sr.Close();

            LanguagePicker.SelectedIndex = saving.language;

            if (saving.darkMode)
            {
                DarkModeCheckBox.IsChecked = true;
            }

            _timePicker.Time = new TimeSpan(Convert.ToInt32(saving.alarmTime.Hour), Convert.ToInt32(saving.alarmTime.Minute), Convert.ToInt32(saving.alarmTime.Second));

            if (saving.alarmNote != "")
            {
                _entry.Text = saving.alarmNote;
            }
        }

        void SaveData()
        {
            StreamWriter writer = new StreamWriter(_filePath, false);
            {
                writer.WriteLine(saving.darkMode);
                writer.WriteLine(_timePicker.Time);
                writer.WriteLine(saving.language);
                writer.WriteLine(saving.alarmNote);
                writer.Close();
            }
        }

        void OpenMainPage(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
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
            _triggerTime = DateTime.Today + _timePicker.Time;
            if (_triggerTime < DateTime.Now)
            {
                _triggerTime += TimeSpan.FromDays(1);
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
                barColour = "LightGray";
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
                TimerHeader.Text = "Budík je nastavený na:";
                _entry.Placeholder = "Tu nastavte poznámku";
                SaveButton.Text = "ULOŽIŤ";
                DarkModeText.Text = "Tmavý režim";
                LanguageText.Text = "Jazyk";
                saving.language = 0;
            }
            else
            {
                Header_Description.Text = "Settings";
                TimerHeader.Text = "The alarm is set to:";
                _entry.Placeholder = "Set a note here";
                SaveButton.Text = "SAVE";
                DarkModeText.Text = "Dark mode";
                LanguageText.Text = "Language";
                saving.language = 1;
            }
            SaveData();
        }
        private void AlarmNoteSave(object sender, EventArgs e)
        {
            saving.alarmNote = _entry.Text;
            SaveData();
        }
    }
}
