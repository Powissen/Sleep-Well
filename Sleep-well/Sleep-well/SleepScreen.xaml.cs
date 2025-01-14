﻿
using Plugin.LocalNotification;
using Plugin.SimpleAudioPlayer;
using SleepWell.Services;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepWell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepScreen : ContentPage
    {
        private bool sleeping;
        private int[] sleepMusicCycles = new int[] { 0, 0 };
        Saving saving = new Saving();
        public string Time { get; set; } = "";
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
        public DateTime TimeWhenTimerEnabled;
        private bool popupShowed;
        string notificationTitle;
        string notificationDescription;
        public static bool shouldTickTimer;

        public SleepScreen()
        {
            InitializeComponent();

            sleeping = true;
            popupShowed = false;
            BindingContext = this;
            shouldTickTimer = true;
            TimeWhenTimerEnabled = DateTime.Now;

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

            if (saving.musicToSleep)
            {
                sleepMusicCycles[0] = saving.fallAsleepTime / 5;
                player.Load("SleepMusic.mp3");
                player.Volume = 50;
                player.Play();
                sleepMusicCycles[1]++;
            }

            if (saving.language == 1)
            {
                Warning.Text = "Please leave the app opened, turn on the sound for the apps and turn off the screen";
                if (saving.alarmTime.Minute < 10)
                    alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                else
                    alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
            }
            else
            {
                if (saving.alarmTime.Minute < 10)
                    alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                else
                    alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
            }

            while (saving.alarmTime <= DateTime.Now)
            {
                saving.alarmTime = saving.alarmTime.AddDays(1);
            }


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

            //if (saving.language == 1)
            //{
            //    DisplayAlert("Warning", "Your device is running an Android version older than 8.0! The alarm may not work properly!", "OK");
            //}
            //else
            //{
            //    DisplayAlert("Upozornenie", "Vaše zariadenie používa verziu Androidu staršiu než 8.0! Budík nemusí správne fungovať!", "OK");
            //}

            DependencyService.Resolve<IForegroundService>().StartMyForegroundService();
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
            OnTimerTick();
        }


        public bool OnTimerTick()
        {
            if (sleeping && saving.musicToSleep && sleepMusicCycles[1] < sleepMusicCycles[0] && !player.IsPlaying)
            {
                player.Play();
                sleepMusicCycles[1]++;
            }

            if (DateTime.Now.Minute < 10)
                Time = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            else
                Time = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();

            OnPropertyChanged(nameof(Time));
            TimeSpan after = saving.alarmTime.Subtract(DateTime.Now);


            if (saving.language == 1)
                alarmAfter.Text = "Alarm after: " + after.Hours + " hours and " + after.Minutes + " minutes";
            else
                alarmAfter.Text = "Budík za: " + after.Hours + " hodín a " + after.Minutes + " minút";




            if (DateTime.Now >= saving.alarmTime && sleeping)
            {
                Vibration.Vibrate();

                if (!popupShowed)
                {
                    popupShowed = true;
                    player.Stop();
                    switch (saving.alarmSound)
                    {
                        case 0: player.Load("Classic.mp3"); break;
                        case 1: player.Load("Nature.mp3"); break;
                        case 2: player.Load("ChillMusic.mp3"); break;
                        case 3: player.Load("Guitar.mp3"); break;
                    }
                    player.Volume = 100;
                    player.Play();
                    player.Loop = true;
                    showPopup();

                    if (saving.language == 1)
                    {
                        notificationTitle = "Good morning :-)";
                        notificationDescription = "The alarm was turned on, another great day is ahead of us!";
                    }
                    else
                    {
                        notificationTitle = "Dobré ráno :-)";
                        notificationDescription = "Budík bol zapnutý, ďalší skvelý deň je pred nami!";
                    }

                    var notification = new NotificationRequest
                    {
                        BadgeNumber = 1,
                        Title = notificationTitle,
                        Description = notificationDescription,
                        NotificationId = 1337,

                    };
                    NotificationCenter.Current.Show(notification);
                }
            }
            return true;
        }

        public async void showPopup()
        {
            if (saving.language == 1)
            {
                var result = await DisplayAlert("Good morning :-)", "Note: " + saving.alarmNote, "Add 5 minutes", "Turn off alarm");
                if (result)
                {
                    saving.alarmTime = saving.alarmTime.AddMinutes(5);
                    player.Stop();
                    sleepMusicCycles[0] = 0;
                    popupShowed = false;

                    if (saving.alarmTime.Minute < 10)
                        alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                    else
                        alarmTime.Text = "The alarm is set to: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
                }
                else
                {
                    player.Stop();
                    OpenMainPage();
                }
            }
            else
            {
                var result = await DisplayAlert("Dobré ráno :-)", "Poznámka: " + saving.alarmNote, "Posunúť o 5 minút", "Vypnúť budík");
                if (result)
                {
                    saving.alarmTime = saving.alarmTime.AddMinutes(5);
                    player.Stop();
                    sleepMusicCycles[0] = 0;
                    popupShowed = false;

                    if (saving.alarmTime.Minute < 10)
                        alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":0" + saving.alarmTime.Minute.ToString();
                    else
                        alarmTime.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
                }
                else
                {
                    player.Stop();
                    OpenMainPage();
                }
            }
        }

        void OpenMainPage()
        {
            DependencyService.Resolve<IForegroundService>().StopMyForegroundService();
            TimeSpan sleepLength = DateTime.Now.Subtract(TimeWhenTimerEnabled);

            float sleepCycles = sleepLength.Minutes + sleepLength.Hours * 60;
            if (sleepCycles > 15)
            {
                sleepCycles -= 15;
            }
            sleepCycles /= 89;
            sleepCycles = (float)Math.Floor(sleepCycles);

            sleeping = false;
            player.Stop();


            if (saving.language == 1)
                DisplayAlert("Sleep summary:", "Sleep length: " + sleepLength.Hours + "h " + sleepLength.Minutes + "m " + sleepLength.Seconds + "s \n" + "Sleep cycles: " + sleepCycles.ToString(), "OK");
            else
                DisplayAlert("Zhrnutie spánku:", "Dĺžka spánku: " + sleepLength.Hours + "h " + sleepLength.Minutes + "m " + sleepLength.Seconds + "s \n" + "Spánkových cyklov: " + sleepCycles.ToString(), "OK");


            App.Current.MainPage = new MainPage();
        }


        public void OpenMainPageButton(object sender, EventArgs args)
        {
            OpenMainPage();
        }
    }
}