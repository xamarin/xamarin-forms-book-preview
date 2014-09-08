using System;
using Xamarin.Forms;

namespace NoteTaker10
{
    public class App
    {
        static NoteFolder noteFolder = new NoteFolder();

        internal static NoteFolder NoteFolder
        {
            get { return noteFolder; }
        }

        public static Page GetMainPage()
        {
            return new NavigationPage(new HomePage());
        }
    }
}
