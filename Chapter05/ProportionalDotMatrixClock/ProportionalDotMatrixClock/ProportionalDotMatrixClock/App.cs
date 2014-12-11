using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ProportionalDotMatrixClock
{
    public class App : Application
    {
        public App ()
        {
            MainPage = new ProportionalDotMatrixClockPage();
        }
    }
}
