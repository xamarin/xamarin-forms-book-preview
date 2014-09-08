using System;
using Xamarin.Forms;

namespace NoteTaker7
{
    class NotePage : ContentPage
    {
        string filename;
        Note note = new Note();

        public NotePage(string filename)
        {
            this.filename = filename;
            Title = "New Note";

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

            // Set data bindings.
            this.BindingContext = note;
            entry.SetBinding(Entry.TextProperty, "Title");
            editor.SetBinding(Editor.TextProperty, "Text");

            // Asssemble page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    new Label { Text = "Title:" },
                    entry,
                    new Label { Text = "Note:" },
                    editor,
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Only save it if there's some text somewhere.
            if (!String.IsNullOrWhiteSpace(note.Title) ||
                !String.IsNullOrWhiteSpace(note.Text))
            {
                note.SaveAsync(filename);
            }
            base.OnDisappearing();
        }
    }
}
