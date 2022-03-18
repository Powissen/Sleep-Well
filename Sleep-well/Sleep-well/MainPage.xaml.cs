using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sleep_well
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //test
        }
        void RefreshTime(object sender, EventArgs args)
        {
            time.Text = (DateTime.Now.Hour + ":" + DateTime.Now.Minute).ToString();

            int hours;
            int minutes;

            hours = 23 - DateTime.Now.Hour + 6;
            minutes = 60 - DateTime.Now.Minute + 0;

            if (minutes > 60)
            {
                hours++;
                minutes -= 60;
            }

            alarmAfter.Text = "Budík o " + hours + " hodín a " + minutes + " minút";
        }
    }
}
