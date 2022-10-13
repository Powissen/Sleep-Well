using SleepWell.Services;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace SleepWell
{
    public partial class MainPage : ContentPage
    {
        Saving saving = new Saving();
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
        public string Time { get; set; } = "";

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt"));


            if (!File.Exists(_filePath))
            {
                saving.darkMode = true;
                saving.alarmTime = DateTime.Now;
                saving.language = 0;
                saving.alarmNote = "";
                saving.alarmSound = 2;
                saving.fallAsleepTime = 15;
                saving.musicToSleep = true;
                saving.builtinTimer = false;

                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    writer.WriteLine(saving.darkMode);
                    writer.WriteLine(saving.alarmTime);
                    writer.WriteLine(saving.language);
                    writer.WriteLine("");
                    writer.WriteLine(saving.alarmSound);
                    writer.WriteLine(saving.fallAsleepTime);
                    writer.WriteLine(saving.musicToSleep);
                    writer.Write(saving.builtinTimer);
                    writer.Close();
                }
            }

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
                SleepButton.Text = "I GO TO SLEEP";
                WhenToSleepButton.Text = "WHEN TO GO TO BED?";
            }
            _timePicker.Time = new TimeSpan(Convert.ToInt32(saving.alarmTime.Hour), Convert.ToInt32(saving.alarmTime.Minute), Convert.ToInt32(saving.alarmTime.Second));
        }

        void OpenMenu(object sender, EventArgs args)
        {
            App.Current.MainPage = new Menu();
        }
        void OpenWhenToSleep(object sender, EventArgs args)
        {
            App.Current.MainPage = new WhenToSleep();
        }
        void StartSleeping(object sender, EventArgs args)
        {
            if (saving.builtinTimer)
                App.Current.MainPage = new SleepScreen();
            else
                DependencyService.Resolve<IForegroundService>().OpenAlarmClock();
        }


        void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                StreamWriter writer = new StreamWriter(_filePath, false);
                {
                    writer.WriteLine(saving.darkMode);
                    writer.WriteLine(_timePicker.Time);
                    writer.WriteLine(saving.language);
                    writer.WriteLine(saving.alarmNote);
                    writer.WriteLine(saving.alarmSound);
                    writer.WriteLine(saving.fallAsleepTime);
                    writer.WriteLine(saving.musicToSleep);
                    writer.Write(saving.builtinTimer);
                    writer.Close();
                }
            }
        }
    }
}

