using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DigitalClock
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new DigitalClockPage();
        }
    }
}
