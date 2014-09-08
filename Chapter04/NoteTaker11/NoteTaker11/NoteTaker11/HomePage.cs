using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NoteTaker11
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Note Taker 11";

            // Create and initialize ListView.
            ListView listView = new ListView
            {
                ItemsSource = App.NoteFolder.Notes,
                ItemTemplate = new DataTemplate(typeof(TextCell)),
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "Identifier");

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

            // Create and initialize ToolbarItem.
            ToolbarItem addNewItem = new ToolbarItem
            {
                Name = "Add Note",
                Icon = Device.OnPlatform("new.png", 
                                         "ic_action_new.png", 
                                         "Images/add.png"),
                Order = ToolbarItemOrder.Primary
            };

            addNewItem.Activated += (sender, args) =>
                {
                    // Create unique filename.
                    DateTime datetime = DateTime.UtcNow;
                    string filename = datetime.ToString("yyyyMMddHHmmssfff") + ".note";

                    // Navigate to new NotePage.
                    Note note = new Note(filename);
                    this.Navigation.PushAsync(new NotePage(note));
                };

            this.ToolbarItems.Add(addNewItem);

            // Assemble page.
            this.Content = listView;
        }
    }
}
