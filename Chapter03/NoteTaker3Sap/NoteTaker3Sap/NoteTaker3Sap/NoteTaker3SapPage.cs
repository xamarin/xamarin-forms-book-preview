using System;

#if !WINDOWS_PHONE
using System.IO;
#else
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

using Xamarin.Forms;

namespace NoteTaker3Sap
{
    class NoteTaker3SapPage : ContentPage
    {
        static readonly string FILENAME = "test.note";

        Note note = new Note();
        Entry entry;
        Editor editor;
        Button loadButton;

        public NoteTaker3SapPage()
        {
            // Create Entry and Editor views.
            entry = new Entry
            {
                Placeholder = "Title (optional)"
            };

            editor = new Editor
            {
                Keyboard = Keyboard.Create(KeyboardFlags.All),
                BackgroundColor = Device.OnPlatform(Color.Default,
                                                    Color.Default,
                                                    Color.White),
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Create Save and Load buttons.
            Button saveButton = new Button
            {
                Text = "Save",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            saveButton.Clicked += OnSaveButtonClicked;

            loadButton = new Button
            {
                Text = "Load",
                IsEnabled = false,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            loadButton.Clicked += OnLoadButtonClicked;

            // Check if the file is available.

#if !WINDOWS_PHONE // iOS and Android

            string docsPath = Environment.GetFolderPath(
                                    Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(docsPath, FILENAME);
            loadButton.IsEnabled = File.Exists(filepath);

#else // Windows Phone

            StorageFolder localFolder = 
                    ApplicationData.Current.LocalFolder;
            IAsyncOperation<StorageFile> createOp = 
                    localFolder.GetFileAsync(FILENAME);
            createOp.Completed = (asyncInfo, asyncStatus) =>
            {
                loadButton.IsEnabled = asyncStatus != AsyncStatus.Error;
            };
     
#endif

            // Assemble page.
            this.Padding =
                new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);

            this.Content = new StackLayout
            {
                Children = 
                {
                    new Label 
                    { 
                        Text = "Title:" 
                    },
                    entry,
                    new Label 
                    { 
                        Text = "Note:" 
                    },
                    editor,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = 
                        {
                            saveButton,
                            loadButton
                        }
                    }
                }
            };
        }

        void OnSaveButtonClicked(object sender, EventArgs args)
        {
            note.Title = entry.Text;
            note.Text = editor.Text;
            note.Save(FILENAME);
            this.loadButton.IsEnabled = true;
        }

        void OnLoadButtonClicked(object sender, EventArgs args)
        {
            note.Load(FILENAME);
            entry.Text = note.Title;
            editor.Text = note.Text;
        }
    }
}
