using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SleepWell
{
    public partial class MainPage : ContentPage
    {
        bool bud = true;
        string s = "";
        int Hoursbudik;
        int Minutesbudik;

        public MainPage()
        {
            InitializeComponent();
        }

        void OpenSettings(object sender, EventArgs args)
        {
            App.Current.MainPage = new Settings();
        }


        void RefreshTime(object sender, EventArgs args)
        {

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

            alarmAfter.Text = s;

            if (Minutesbudik == DateTime.Now.Minute)
            {
                if (Hoursbudik == DateTime.Now.Hour)
                {
                    alarmAfter.Text = ("vstávaj");
                }
            }
        }

    }
}
