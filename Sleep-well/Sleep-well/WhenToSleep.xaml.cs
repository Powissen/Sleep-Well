﻿
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SleepWell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhenToSleep : ContentPage
    {
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
        Saving saving = new Saving();
        string alarmTime;
        public string textColour { get; set; } = "Black";
        public WhenToSleep()
        {
            InitializeComponent();
            BindingContext = this;


            StreamReader sr = new StreamReader(_filePath);
            //saving.alarmEnabled = Convert.ToBoolean(sr.ReadLine());
            saving.darkMode = Convert.ToBoolean(sr.ReadLine());
            saving.alarmTime = DateTime.Parse(sr.ReadLine());
            saving.language = Convert.ToInt32(sr.ReadLine());
            saving.alarmNote = sr.ReadLine();
            sr.Close();

            if (saving.darkMode)
            {
                BackgroundColor = Color.FromHex("1f1f1f");
                textColour = "LightGray";
                OnPropertyChanged(nameof(textColour));
            }


            if (saving.alarmTime.Minute < 10)
            {
                alarmTime = saving.alarmTime.Hour + ":" + saving.alarmTime.Minute + "0".ToString();
            }
            else
            {
                alarmTime = saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
            }

            if (saving.language == 1)
            {
                AlarmTime.Text = "The alarm is set to:\n" + alarmTime;
            }
            else
            {
                AlarmTime.Text = "Budík je nastavený na:\n" + alarmTime;
            }


            DateTime time6 = saving.alarmTime.AddMinutes(-90 - 15);
            if (time6.Minute < 10)
            {
                Time6.Text = time6.Hour + ":" + time6.Minute + "0".ToString();
            }
            else
            {
                Time6.Text = time6.Hour + ":" + time6.Minute.ToString();
            }

            DateTime time5 = saving.alarmTime.AddMinutes(-180 - 15);
            if (time5.Minute < 10)
            {
                Time5.Text = time5.Hour + ":" + time5.Minute + "0".ToString();
            }
            else
            {
                Time5.Text = time5.Hour + ":" + time5.Minute.ToString();
            }

            DateTime time4 = saving.alarmTime.AddMinutes(-270 - 15);
            if (time4.Minute < 10)
            {
                Time4.Text = time4.Hour + ":" + time4.Minute + "0".ToString();
            }
            else
            {
                Time4.Text = time4.Hour + ":" + time4.Minute.ToString();
            }

            DateTime time3 = saving.alarmTime.AddMinutes(-360 - 15);
            if (time3.Minute < 10)
            {
                Time3.Text = time3.Hour + ":" + time3.Minute + "0".ToString();
            }
            else
            {
                Time3.Text = time3.Hour + ":" + time3.Minute.ToString();
            }

            DateTime time2 = saving.alarmTime.AddMinutes(-450 - 15);
            if (time2.Minute < 10)
            {
                Time2.Text = time2.Hour + ":" + time2.Minute + "0".ToString();
            }
            else
            {
                Time2.Text = time2.Hour + ":" + time2.Minute.ToString();
            }

            DateTime time1 = saving.alarmTime.AddMinutes(-540 - 15);
            if (time1.Minute < 10)
            {
                Time1.Text = time1.Hour + ":" + time1.Minute + "0".ToString();
            }
            else
            {
                Time1.Text = time1.Hour + ":" + time1.Minute.ToString();
            }
        }


        void TipsForSleep(object sender, EventArgs args)
        {   
            if (saving.language == 1)
            {
                DisplayAlert("What to do for a good sleep? \n\n", "• Don't use the phone at bedtime \n• Observe the regular sleep time \n• Don't perform physical activity before bedtime \n• Don't go to bed with a full stomach \n• Ventilate the room before going to bed \n", "OK");
            }
            else
            {
                DisplayAlert("Čo robiť pre dobrý spánok? \n\n", "• Nepoužívať pred spaním mobil \n• Dodržiavať pravidený čas zaspatia \n• Nevykonávať pred spánkom fyzickú aktivitu \n• Nechodiť do postele s plným bruchom \n• Vyvetrať miestnosť pred spánkom", "OK");
            }
        }


        void OpenMainPage(object sender, EventArgs args)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}