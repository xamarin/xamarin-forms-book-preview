using System;
using Xamarin.Forms;

namespace NoteTaker12
{
    public class App : Application
    {
        static NoteFolder noteFolder = new NoteFolder();

        internal static readonly string TransientFilename = "TransientData.save";

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
