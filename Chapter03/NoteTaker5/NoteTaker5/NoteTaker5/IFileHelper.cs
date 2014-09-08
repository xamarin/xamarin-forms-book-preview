using System;

namespace NoteTaker5
{
    public interface IFileHelper
    {
        void Exists(string filename, Action<bool> completed);

        void WriteAllText(string filename, string text, Action completed);

        void ReadAllText(string filename, Action<string> completed);
    }
}
