namespace NoteTaker2Pcl
{
    public interface IFileHelper
    {
        bool Exists(string filename);

        void WriteAllText(string filename, string text);

        string ReadAllText(string filename);
    }
}
