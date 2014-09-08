using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using PlatformHelpers;

namespace NoteTaker8
{
    class Note : INotifyPropertyChanged
    {
        string title, text;

        public event PropertyChangedEventHandler PropertyChanged;

        public Note(string filename)
        {
            this.Filename = filename;
        }

        public string Filename { private set; get; }

        public string Title 
        {
            set { SetProperty(ref title, value); }
            get { return title; }
        }

        public string Text
        {
            set { SetProperty(ref text, value); }
            get { return text; }
        }

        public Task SaveAsync()
        {
            string text = this.Title + "\n" + this.Text;
            return FileHelper.WriteTextAsync(this.Filename, text);
        }

        public async Task LoadAsync()
        {
            string text = await FileHelper.ReadTextAsync(this.Filename);

            // Break string into Title and Text.
            int index = text.IndexOf('\n');
            this.Title = text.Substring(0, index);
            this.Text = text.Substring(index + 1);
        }

        bool SetProperty<T>(ref T storage, T value,
                            [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            if (!String.IsNullOrWhiteSpace(this.Title))
                return this.Title;

            int truncationLength = 30;

            if (this.Text.Length <= truncationLength)
                return this.Text;

            string truncated =
                this.Text.Substring(0, truncationLength);

            int index = truncated.LastIndexOf(' ');

            if (index != -1)
                truncated = truncated.Substring(0, index);

            return truncated;
        }
    }
}
