using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker2Pcl.WinPhone.FileHelper))]

namespace NoteTaker2Pcl.WinPhone
{
    class FileHelper : IFileHelper
    {
        public bool Exists(string filename)
        {
            return File.Exists(filename);
        }

        public void WriteAllText(string filename, string text)
        {
            StreamWriter writer = File.CreateText(filename);
            writer.Write(text);
            writer.Close();
        }

        public string ReadAllText(string filename)
        {
            StreamReader reader = File.OpenText(filename);
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }
    }
}
