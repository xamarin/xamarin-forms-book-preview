using System;
using Xamarin.Forms;

namespace Greetings
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new GreetingsPage();
        }
    }
}
