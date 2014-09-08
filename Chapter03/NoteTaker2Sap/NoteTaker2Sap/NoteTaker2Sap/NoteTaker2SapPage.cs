using System;
using Xamarin.Forms;

namespace NoteTaker2Sap
{
    class NoteTaker2SapPage : ContentPage
    {
        static readonly string FILENAME = "test.note";

        Entry entry;
        Editor editor;
        Button loadButton;

        public NoteTaker2SapPage()
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
                IsEnabled = FileHelper.Exists(FILENAME),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            loadButton.Clicked += OnLoadButtonClicked;

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
            Note note = new Note
            {
                Title = entry.Text,
                Text = editor.Text
            };
            note.Save(FILENAME);
            loadButton.IsEnabled = true;
        }

        void OnLoadButtonClicked(object sender, EventArgs args)
        {
            Note note = new Note();
            note.Load(FILENAME);
            entry.Text = note.Title;
            editor.Text = note.Text;
        }
    }
}
