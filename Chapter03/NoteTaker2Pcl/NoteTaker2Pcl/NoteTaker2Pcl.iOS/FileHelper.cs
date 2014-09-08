using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker2Pcl.iOS.FileHelper))]

namespace NoteTaker2Pcl.iOS
{
    class FileHelper : IFileHelper
    {
        public bool Exists(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.Exists(filepath);
        }

        public void WriteAllText(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            File.WriteAllText(filepath, text);
        }

        public string ReadAllText(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.ReadAllText(filepath);
        }

        string GetFilePath(string filename)
        {
            string docsPath = Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docsPath, filename);
        }
    }
}