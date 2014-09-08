using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker4.Droid.FileHelper))]

namespace NoteTaker4.Droid
{
    class FileHelper : IFileHelper
    {
        public void Exists(string filename, Action<bool> completed)
        {
            bool exists = File.Exists(GetFilePath(filename));
            completed(exists);
        }

        public void WriteAllText(string filename, string text, 
                                 Action completed)
        {
            File.WriteAllText(GetFilePath(filename), text);
            completed();
        }

        public void ReadAllText(string filename, Action<string>completed)
        {
            string text = File.ReadAllText(GetFilePath(filename));
            completed(text);
        }

        string GetFilePath(string filename)
        {
            string docsPath = Environment.GetFolderPath(
                                Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docsPath, filename);
        }
    }
}
