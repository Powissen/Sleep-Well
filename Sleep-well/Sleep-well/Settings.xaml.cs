using Plugin.SimpleAudioPlayer;
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
        Saving saving = new Saving();
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;



        public Settings()
        {
            InitializeComponent();
            BindingContext = this;

            LanguagePicker.Items.Add("Slovenčina");
            //LanguagePicker.Items.Add("English");

            FallAsleepTimePicker.Items.Add("5m");
            FallAsleepTimePicker.Items.Add("10m");
            FallAsleepTimePicker.Items.Add("15m");
            FallAsleepTimePicker.Items.Add("20m");
            FallAsleepTimePicker.Items.Add("25m");
            FallAsleepTimePicker.Items.Add("30m");

            AlarmTypePicker.Items.Add("Externý        (Odporúčané)");
            AlarmTypePicker.Items.Add("V aplikácii   (Experimentálne)");


            BackgroundColor = Color.FromHex("1f1f1f");
            textColour = "LightGray";
            barColour = "Black";
            OnPropertyChanged(nameof(textColour));
            OnPropertyChanged(nameof(barColour));
            saving.darkMode = true;


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

            LanguagePicker.SelectedIndex = saving.language;
            SoundPicker.SelectedIndex = saving.alarmSound;
            switch (saving.fallAsleepTime)
            {
                case 5: FallAsleepTimePicker.SelectedIndex = 0; break;
                case 10: FallAsleepTimePicker.SelectedIndex = 1; break;
                case 15: FallAsleepTimePicker.SelectedIndex = 2; break;
                case 20: FallAsleepTimePicker.SelectedIndex = 3; break;
                case 25:FallAsleepTimePicker.SelectedIndex = 4; break;
                case 30:FallAsleepTimePicker.SelectedIndex = 5; break;
            }

            if (saving.builtinTimer)
                AlarmTypePicker.SelectedIndex = 1;
            else
                AlarmTypePicker.SelectedIndex = 0;


            if (saving.musicToSleep)
            {
                SleepMusicCheckBox.IsChecked = true;
            }


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
                writer.WriteLine(saving.alarmTime);
                writer.WriteLine(saving.language);
                writer.WriteLine(saving.alarmNote);
                writer.WriteLine(saving.alarmSound);
                writer.WriteLine(saving.fallAsleepTime);
                writer.WriteLine(saving.musicToSleep);
                writer.Write(saving.builtinTimer);
                writer.Close();
            }
        }
        
        //Multilanguage:  https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/text?pivots=windows 

        private void LanguageChange(object sender, EventArgs e)
        {
            if (LanguagePicker.SelectedIndex == 0)
            {
                Header_Description.Text = "Nastavenia";
                _entry.Placeholder = "Tu nastavte poznámku";
                SaveButton.Text = "ULOŽIŤ";
                FallAsleepTimeText.Text = "Dĺžka zaspávania";
                SleepMusicText.Text = "Hudba na zaspávanie";
                LanguageText.Text = "Jazyk";
                SoundText.Text = "Zvuk budíka";
                SoundPicker.Items.Clear();
                SoundPicker.Items.Add("Klasický");
                SoundPicker.Items.Add("Príroda");
                SoundPicker.Items.Add("Hudba");
                SoundPicker.Items.Add("Gitara");
                SoundPicker.SelectedIndex = saving.alarmSound;
                saving.language = 0;
            }
            else
            {
                Header_Description.Text = "Settings";
                _entry.Placeholder = "Set a note here";
                SaveButton.Text = "SAVE";
                FallAsleepTimeText.Text = "Fall asleep time";
                SleepMusicText.Text = "Music for sleep";
                LanguageText.Text = "Language";
                SoundText.Text = "Alarm sound";
                SoundPicker.Items.Clear();
                SoundPicker.Items.Add("Classic");
                SoundPicker.Items.Add("Nature");
                SoundPicker.Items.Add("Music");
                SoundPicker.Items.Add("Guitar");
                SoundPicker.SelectedIndex = saving.alarmSound;
                saving.language = 1;
            }
            SaveData();
        }

        private void SoundChange(object sender, EventArgs e)
        {
            switch (SoundPicker.SelectedIndex)
            {
                case 0:
                    saving.alarmSound = 0;
                    player.Load("Classic.mp3");
                    break;
                case 1:
                    saving.alarmSound = 1;
                    player.Load("Nature.mp3");
                    break;
                case 2:
                    saving.alarmSound = 2;
                    player.Load("ChillMusic.mp3");
                    break;
                case 3:
                    saving.alarmSound = 3;
                    player.Load("Guitar.mp3");
                    break;

            }
            SoundButton.Source = "muted.png";
            SaveData();
        }
        private void PlaySoundButton(object sender, EventArgs e)
        {
            if (player.IsPlaying)
            {
                SoundButton.Source = "muted.png";
                player.Stop();
            }
            else
            {
                SoundButton.Source = "unmuted.png";
                player.Play();
                player.Loop = true;
            }
        }

        private void SleepSoundWarning(object sender, EventArgs e)
        {
            if (saving.language == 1)
            {
                DisplayAlert("How does it work ? \n\n", "During the duration of falling asleep set above, you will hear music for calm and fast falling asleep.", "OK");
            }
            else
            {
                DisplayAlert("Ako to funguje? \n\n", "Počas dĺžky zaspávania nastavenej vyššie bude hrať hudba pre pokojné a rýchle zaspatie.", "OK");
            }
        }


        private void AlarmNoteSave(object sender, EventArgs e)
        {
            saving.alarmNote = _entry.Text;
            SaveData();
        }

        private void FallAsleepTimeChange(object sender, EventArgs e)
        {
            switch (FallAsleepTimePicker.SelectedIndex)
            {
                case 0: saving.fallAsleepTime = 5; break;
                case 1: saving.fallAsleepTime = 10; break;
                case 2: saving.fallAsleepTime = 15; break;
                case 3: saving.fallAsleepTime = 20; break;
                case 4: saving.fallAsleepTime = 25; break;
                case 5: saving.fallAsleepTime = 30; break;
            }
            SaveData();
        }

        private void CheckBox_SleepMusic(object sender, CheckedChangedEventArgs e)
        {
            if (SleepMusicCheckBox.IsChecked)
            {
                saving.musicToSleep = true;
                SaveData();
            }
            else
            {
                saving.musicToSleep = false;
                SaveData();
            }
        }

        private void AlarmTypeChange(object sender, EventArgs e)
        {
            switch (AlarmTypePicker.SelectedIndex)
            {
                case 0: saving.builtinTimer = false; break;
                case 1: saving.builtinTimer = true; break;
            }
            SaveData();
        }



        void OpenMenu(object sender, EventArgs args)
        {
            player.Stop();
            App.Current.MainPage = new Menu();
        }
    }
}
