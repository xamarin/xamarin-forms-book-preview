using System;
using Xamarin.Forms;

namespace NoteTaker5
{
    static class FileHelper
    {
        static IFileHelper fileHelper = DependencyService.Get<IFileHelper>();

        public static void Exists(string filename, Action<bool> completed)
        {
            fileHelper.Exists(filename, completed);
        }

        public static void WriteAllText(string filename, string text, Action completed)
        {
            fileHelper.WriteAllText(filename, text, completed);
        }

        public static void ReadAllText(string filename, Action<string> completed)
        {
            fileHelper.ReadAllText(filename, completed);
        }

    }
}
