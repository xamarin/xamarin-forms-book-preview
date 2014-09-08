using System;
using Xamarin.Forms;

namespace NoteTaker7
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new NavigationPage(new HomePage());
        }
    }
}
