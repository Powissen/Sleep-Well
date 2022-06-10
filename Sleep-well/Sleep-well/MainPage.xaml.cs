using System;
using System.IO;
using System.Linq;
using System.Collections;
using Xamarin.Forms;
using Plugin.SharedTransitions;

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
            OnTimerTick();
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt"));

            if (!File.Exists(_filePath))
            {
                saving.darkMode = true;
                saving.alarmTime = DateTime.Now;
                saving.language = 0;
                saving.alarmNote = "";
                saving.alarmSound = 2;
                saving.fallAsleepTime = 15;

                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    writer.WriteLine(saving.darkMode);
                    writer.WriteLine(saving.alarmTime);
                    writer.WriteLine(saving.language);
                    writer.WriteLine("");
                    writer.WriteLine(saving.alarmSound);
                    writer.WriteLine(saving.fallAsleepTime);
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
            sr.Close();

            if (saving.language == 1)
            {
                if (saving.alarmTime.Minute < 10)
                {
                    alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                }
                else
                {
                    alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
                }
                SleepButton.Text = "I GO TO SLEEP";
                WhenToSleepButton.Text = "WHEN TO GO TO BED?";
            }
            else
            {
                if (saving.alarmTime.Minute < 10)
                {
                    alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                }
                else
                {
                    alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
                }
            }
        }

        async void OpenSettings(object sender, EventArgs args)
        {
            App.Current.MainPage = new Settings();

        }
        void OpenWhenToSleep(object sender, EventArgs args)
        {
            App.Current.MainPage = new WhenToSleep();
        }
        void StartSleeping(object sender, EventArgs args)
        {
            App.Current.MainPage = new SleepScreen();
        }

        bool OnTimerTick()
        {
            if (DateTime.Now.Minute < 10)
            {
                Time = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            }
            else
            {

                Time = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();
            }
            OnPropertyChanged(nameof(Time));
            return true;
        }
    }
}
