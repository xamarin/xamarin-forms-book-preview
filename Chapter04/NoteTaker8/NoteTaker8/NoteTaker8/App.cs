using System;
using Xamarin.Forms;

namespace NoteTaker8
{
    public class App : Application
    {
        static NoteFolder noteFolder = new NoteFolder();

        internal static NoteFolder NoteFolder
        {
            get { return noteFolder; }
        }

        public App ()
        {
            MainPage = new NavigationPage(new HomePage());
        }
    }
}
