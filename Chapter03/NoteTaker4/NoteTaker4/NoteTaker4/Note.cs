using System;
using System.ComponentModel;

namespace NoteTaker4
{
    class Note : INotifyPropertyChanged
    {
        string title, text;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title 
        {
            set
            {
                if (title != value)
                {
                    title = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, 
                                new PropertyChangedEventArgs("Title"));
                    }
                }
            }
            get
            {
                return title;    
            }
        }

        public string Text
        {
            set
            {
                if (text != value)
                {
                    text = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, 
                                new PropertyChangedEventArgs("Text"));
                    }
                }
            }
            get
            {
                return text;
            }
        }

        public void Save(string filename)
        {
            string text = this.Title + "\n" + this.Text;
            FileHelper.WriteAllText(filename, text, () => { });
        }

        public void Load(string filename)
        {
            FileHelper.ReadAllText(filename, (string text) =>
                {
                    // Break string into Title and Text.
                    int index = text.IndexOf('\n');
                    this.Title = text.Substring(0, index);
                    this.Text = text.Substring(index + 1);
                });
        }
    }
}
