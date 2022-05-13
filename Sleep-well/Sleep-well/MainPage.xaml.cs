using System;
using System.IO;
using System.Linq;
using System.Collections;
using Xamarin.Forms;

namespace SleepWell
{
    public partial class MainPage : ContentPage
    {
        Saving saving = new Saving();
        string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt");
        public string Time { get; set; } = "";
        bool bud = true;
        string s = "";
        int Hoursbudik;
        int Minutesbudik;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            if (DateTime.Now.Minute < 10)
            {
                Time = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            }
            else
            {

                Time = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();
            }
            OnPropertyChanged(nameof(Time));

            //File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dat.txt"));

            if (!File.Exists(_filePath))
            {
                saving.alarmEnabled = false;
                saving.darkMode = false;
                saving.alarmTime = DateTime.Now;
                saving.language = 0;
                //saving.alarmNote = "";

                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    writer.WriteLine(saving.alarmEnabled);
                    writer.WriteLine(saving.darkMode);
                    writer.WriteLine(saving.alarmTime);
                    writer.WriteLine(saving.language);
                    //writer.WriteLine("");
                    writer.Close();
                }
            }

            StreamReader sr = new StreamReader(_filePath);
            saving.alarmEnabled = Convert.ToBoolean(sr.ReadLine());
            saving.darkMode = Convert.ToBoolean(sr.ReadLine());
            saving.alarmTime = DateTime.Parse(sr.ReadLine());
            saving.language = Convert.ToInt32(sr.ReadLine());
            //saving.alarmNote = sr.ReadLine();
            sr.Close();

            if (saving.alarmEnabled)
            {
                alarmEnabled.Text = "Budík je nastavený na: " + saving.alarmTime.Hour + ":" + saving.alarmTime.Minute.ToString();
            }
        }

        void OpenSettings(object sender, EventArgs args)
        {
            App.Current.MainPage = new Settings();
        }


        void RefreshTime(object sender, EventArgs args)
        {
            //if (DateTime.Now.Minute < 10)
            //{
            //    time.Text = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            //}
            //else
            //{

            //    time.Text = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();
            //}
            int hours;
            int minutes;

            hours = 23 - DateTime.Now.Hour + 6;
            minutes = 60 - DateTime.Now.Minute + 0;

            if (minutes > 60)
            {
                hours++;
                minutes -= 60;
            }
            s = "nie je budik";
            if (bud)
            {
                s = "nie je budik";
            }
            else
            {
                if ((-(DateTime.Now.Minute - Minutesbudik)) < 0)
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }
                else
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }
            }

            alarmAfter.Text = s;

            if (Minutesbudik == DateTime.Now.Minute)
            {
                if (Hoursbudik == DateTime.Now.Hour)
                {
                    alarmAfter.Text = ("vstávaj");

                }
            }
            if (Minutesbudik + 1 == DateTime.Now.Minute)
            {
                if (Hoursbudik == DateTime.Now.Hour)
                {
                    alarmAfter.Text = ("nie je budik");

                }
            }

        }

        void o10(object sender, EventArgs args)
        {
            int hours2 = DateTime.Now.Hour;
            int minutes2 = DateTime.Now.Minute;
            if (DateTime.Now.Minute < 49)
            {
                minutes2 = DateTime.Now.Minute + 10;
            }
            else
            {
                hours2++;
                minutes2 = minutes2 - 50;
            }
            hours2 = hours2 + 9;
            if (hours2 > 24)
            {
                hours2 = hours2 - 24;
            }


            Hoursbudik = hours2;
            Minutesbudik = minutes2;
            bud = false;
            if (DateTime.Now.Minute < 10)
            {
                time.Text = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            }
            else
            {

                time.Text = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();
            }
            int hours;
            int minutes;

            hours = 23 - DateTime.Now.Hour + 6;
            minutes = 60 - DateTime.Now.Minute + 0;

            if (minutes > 60)
            {
                hours++;
                minutes -= 60;
            }
            s = "nie je budik";
            if (bud)
            {
                s = "nie je budik";
            }
            else
            {

                if ((-(DateTime.Now.Minute - Minutesbudik)) < 0)
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }
                else
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }



            }

            alarmAfter.Text = s;

            if (Minutesbudik == DateTime.Now.Minute)
            {
                if (Hoursbudik == DateTime.Now.Hour)
                {
                    alarmAfter.Text = ("vstávaj");
                }
            }

        }
        void o0(object sender, EventArgs args)
        {
            int hours2 = DateTime.Now.Hour;
            int minutes2 = DateTime.Now.Minute;
            if (DateTime.Now.Minute < 49)
            {
                minutes2 = DateTime.Now.Minute;
            }
            else
            {
                hours2++;
                minutes2 = minutes2 - 60;
            }
            hours2 = hours2 + 9;
            if (hours2 > 24)
            {
                hours2 = hours2 - 24;
            }


            Hoursbudik = hours2;
            Minutesbudik = minutes2;
            bud = false;
            if (DateTime.Now.Minute < 10)
            {
                time.Text = (DateTime.Now.Hour + ":0" + DateTime.Now.Minute).ToString();
            }
            else
            {

                time.Text = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();
            }
            int hours;
            int minutes;

            hours = 23 - DateTime.Now.Hour + 6;
            minutes = 60 - DateTime.Now.Minute + 0;

            if (minutes > 60)
            {
                hours++;
                minutes -= 60;
            }
            s = "nie je budik";
            if (bud)
            {
                s = "nie je budik";
            }
            else
            {

                if ((-(DateTime.Now.Minute - Minutesbudik)) < 0)
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour - 1).ToString() + " hodín a " + (60 - (DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }
                else
                {
                    if (DateTime.Now.Hour > 15)
                    {
                        s = "Budík o " + (24 - DateTime.Now.Hour + Hoursbudik).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                    else
                    {
                        s = "Budík o " + (Hoursbudik - DateTime.Now.Hour).ToString() + " hodín a " + (-(DateTime.Now.Minute - Minutesbudik)).ToString() + " minút.";
                    }
                }
            }

        }
    }
}
