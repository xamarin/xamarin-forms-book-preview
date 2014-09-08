using System;
using Xamarin.Forms;

namespace NoteTaker2Pcl
{
    class Note
    {
        public string Title { set; get; }

        public string Text { set; get; }

        public void Save(string filename)
        {
            string text = this.Title + "\n" + this.Text;
            DependencyService.Get<IFileHelper>().
                                        WriteAllText(filename, text);
        }

        public void Load(string filename)
        {
            string text = DependencyService.Get<IFileHelper>().
                                        ReadAllText(filename);

            // Break string into Title and Text.
            int index = text.IndexOf('\n');
            this.Title = text.Substring(0, index);
            this.Text = text.Substring(index + 1);
        }
    }
}
