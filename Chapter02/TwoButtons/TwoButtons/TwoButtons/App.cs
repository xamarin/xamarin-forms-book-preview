using System;
using Xamarin.Forms;

namespace TwoButtons
{
	public class App : Application
    {
        public static Page GetMainPage()
        {
            return new TwoButtonsPage();
        }
    }
}
