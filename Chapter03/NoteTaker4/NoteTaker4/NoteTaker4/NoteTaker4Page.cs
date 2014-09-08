using System;
using Xamarin.Forms;

namespace NoteTaker4
{
    class NoteTaker4Page : ContentPage
    {
        static readonly string FILENAME = "test.note";

        Note note = new Note();
        Button loadButton;

        public NoteTaker4Page()
        {
            // Create Entry and Editor views.
            Entry entry = new Entry
            {
                Placeholder = "Title (optional)"
            };

            Editor editor = new Editor
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
            FileHelper.Exists(FILENAME, (exists) =>
                {
                    loadButton.IsEnabled = exists;
                });

            // Handle the Note's PropertyChanged event.
            note.PropertyChanged += (sender, args) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                        {
                            switch (args.PropertyName)
                            {
                                case "Title":
                                    entry.Text = note.Title;
                                    break;

                                case "Text":
                                    editor.Text = note.Text;
                                    break;
                            }
                        });
                };

            // Handle the Entry and Editor TextChanged events.
            entry.TextChanged += (sender, args) =>
                {
                    note.Title = args.NewTextValue;
                };

            editor.TextChanged += (sender, args) =>
                {
                    note.Text = args.NewTextValue;
                };

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
            note.Save(FILENAME);
            loadButton.IsEnabled = true;
        }

        void OnLoadButtonClicked(object sender, EventArgs args)
        {
            note.Load(FILENAME);
        }
    }
}
