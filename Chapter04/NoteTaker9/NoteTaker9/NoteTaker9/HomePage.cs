using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NoteTaker9
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Note Taker 9";

            // Create and initialize ListView.
            ListView listView = new ListView
            {
                ItemsSource = App.NoteFolder.Notes,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Handle item selection for editing and deleting.
            listView.ItemSelected += (sender, args) =>
                {
                    if (args.SelectedItem != null)
                    {
                        // Deselect the item.
                        listView.SelectedItem = null;

                        // Navigate to NotePage.
                        Note note = (Note)args.SelectedItem;
                        this.Navigation.PushAsync(new NotePage(note, true));
                    }
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
                    string filename = 
                        datetime.ToString("yyyyMMddHHmmssfff") + ".note";

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
