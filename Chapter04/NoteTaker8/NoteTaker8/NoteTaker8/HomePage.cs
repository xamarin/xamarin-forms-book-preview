using System;
using Xamarin.Forms;

namespace NoteTaker8
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Note Taker 8";

            // Create and initialize ListView.
            ListView listView = new ListView
            {
                ItemsSource = App.NoteFolder.Notes,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Create and initialize Button
            Button button = new Button
            {
                Text = "Add New Note",
                HorizontalOptions = LayoutOptions.Center
            };

            button.Clicked += (sender, args) =>
                {
                    // Create unique filename.
                    DateTime datetime = DateTime.UtcNow;
                    string filename = datetime.ToString("yyyyMMddHHmmssfff") + ".note";

                    // Navigate to new NotePage.
                    Note note = new Note(filename);
                    this.Navigation.PushAsync(new NotePage(note));
                };

            // Assemble page.
            this.Content = new StackLayout
            {
                Children = 
                {
                    listView,
                    button
                }
            };
        }
    }
}
