using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NoteTaker7
{
    class Note : INotifyPropertyChanged
    {
        string title, text;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public Task SaveAsync(string filename)
        {
            string text = this.Title + "\n" + this.Text;
            return FileHelper.WriteTextAsync(filename, text);
        }

        public async Task LoadAsync(string filename)
        {
            string text = await FileHelper.ReadTextAsync(filename);

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
    }
}
