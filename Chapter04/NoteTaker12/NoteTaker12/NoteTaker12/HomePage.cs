using System;
using System.Collections.Generic;
using Xamarin.Forms;

using PlatformHelpers;

namespace NoteTaker12
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            this.Title = "Note Taker 12";

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

            CheckForResumedStartup();
        }

        async void CheckForResumedStartup()
        {
            if (await FileHelper.ExistsAsync(App.TransientFilename))
            {
                // Read the file.
                string str = await FileHelper.ReadTextAsync(App.TransientFilename);

                // Delete the file.
                await FileHelper.DeleteFileAsync(App.TransientFilename);

                // Break down the file contents.
                string[] contents = str.Split('\x1F');
                string filename = contents[0];
                bool isNoteEdit = Boolean.Parse(contents[1]);
                string entryText = contents[2];
                string editorText = contents[3];

                // Create the Note object and initialize it with saved data.
                Note note = new Note(filename);
                note.Title = entryText;
                note.Text = editorText;

                // Navigate to NotePage.
                NotePage notePage = new NotePage(note, isNoteEdit);
                await this.Navigation.PushAsync(notePage);
            }
        }
    }
}
