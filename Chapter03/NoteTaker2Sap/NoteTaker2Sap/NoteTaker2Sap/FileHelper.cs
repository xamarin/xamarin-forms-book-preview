using System;
using System.IO;

namespace NoteTaker2Sap
{
    static class FileHelper
    {

#if !WINDOWS_PHONE // iOS and Android

        public static bool Exists(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.Exists(filepath);
        }

        public static void WriteAllText(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            File.WriteAllText(filepath, text);
        }

        public static string ReadAllText(string filename)
        {
            string filepath = GetFilePath(filename);
            return File.ReadAllText(filepath);
        }

        static string GetFilePath(string filename)
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(docsPath, filename);
        }

#else // Windows Phone

        public static bool Exists(string filename)
        {
            return File.Exists(filename);
        }

        public static void WriteAllText(string filename, string text)
        {
            StreamWriter writer = File.CreateText(filename);
            writer.Write(text);
            writer.Close();
        }

        public static string ReadAllText (string filename)
        {
            StreamReader reader = File.OpenText(filename);
            string text = reader.ReadToEnd();
            reader.Close();
            return text;
        }

#endif

    }
}
