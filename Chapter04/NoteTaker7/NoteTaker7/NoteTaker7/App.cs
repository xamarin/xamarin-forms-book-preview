using System;
using Xamarin.Forms;

namespace NoteTaker7
{
    public class App : Application
    {
        public App ()
        {
            MainPage = new NavigationPage(new HomePage());
        }
    }
}
