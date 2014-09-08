using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using PlatformHelpers;

namespace NoteTaker11
{
    class Note : INotifyPropertyChanged
    {
        string title, text, identifier;

        public event PropertyChangedEventHandler PropertyChanged;

        public Note(string filename)
        {
            this.Filename = filename;
        }

        public string Filename { private set; get; }

        public string Title
        {
            set 
            { 
                if (SetProperty(ref title, value))
                {
                    this.Identifier = MakeIdentifier();
                }
            }
            get { return title; }
        }

        public string Text
        {
            set 
            { 
                if (SetProperty(ref text, value) && 
                    String.IsNullOrWhiteSpace(this.Title))
                {
                    this.Identifier = MakeIdentifier();
                }
            }
            get { return text; }
        }

        public string Identifier
        {
            private set { SetProperty(ref identifier, value); }
            get { return identifier; }
        }

        string MakeIdentifier()
        {
            if (!String.IsNullOrWhiteSpace(this.Title))
                return this.Title;

            int truncationLength = 30;

            if (this.Text == null ||
                this.Text.Length <= truncationLength)
            {
                return this.Text;
            }

            string truncated =
                this.Text.Substring(0, truncationLength);

            int index = truncated.LastIndexOf(' ');

            if (index != -1)
                truncated = truncated.Substring(0, index);

            return truncated;
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

        public async Task DeleteAsync()
        {
            await FileHelper.DeleteFileAsync(this.Filename);
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
    }
}
