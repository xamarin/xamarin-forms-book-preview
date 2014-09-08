using System;
using System.Threading.Tasks;
using Xamarin.Forms;

using PlatformHelpers;

namespace NoteTaker
{
    class NotePage : ContentPage
    {
        Note note;
        bool isNoteEdit;
        ILifecycleHelper helper = DependencyService.Get<ILifecycleHelper>();

        public NotePage(Note note, bool isNoteEdit = false)
        {
            // Initialize Note object
            this.note = note;
            this.isNoteEdit = isNoteEdit;
            Title = isNoteEdit ? "Edit Note" : "New Note";

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

            // Assemble page.
            StackLayout stackLayout = new StackLayout
            {
                Children = 
                {
                    new Label { Text = "Title:" },
                    entry,
                    new Label { Text = "Note:" },
                    editor,
                }
            };

            if (isNoteEdit)
            {
                // Cancel toolbar item.
                ToolbarItem cancelItem = new ToolbarItem
                {
                    Name = "Cancel",
                    Icon = Device.OnPlatform("cancel.png", 
                                             "ic_action_cancel.png", 
                                             "Images/cancel.png"),
                    Order = ToolbarItemOrder.Primary
                };

                cancelItem.Activated += async (sender, args) =>
                    {
                        bool confirm = await this.DisplayAlert("Note Taker", 
                                                               "Cancel note edit?", 
                                                               "Yes", "No");
                        if (confirm)
                        {
                            // Reload note.
                            await note.LoadAsync();

                            // Return to home page.
                            await this.Navigation.PopAsync();
                        }
                    };

                this.ToolbarItems.Add(cancelItem);

                // Delete toolbar item.
                ToolbarItem deleteItem = new ToolbarItem
                {
                    Name = "Delete",
                    Icon = Device.OnPlatform("discard.png", 
                                             "ic_action_discard.png", 
                                             "Images/delete.png"),
                    Order = ToolbarItemOrder.Primary
                };

                deleteItem.Activated += async (sender, args) =>
                    {
                        bool confirm = await this.DisplayAlert("Note Taker", 
                                                               "Delete this note?", 
                                                               "Yes", "No");
                        if (confirm)
                        {
                            // Delete Note file and remove from collection.
                            await note.DeleteAsync();
                            App.NoteFolder.Notes.Remove(note);

                            // Return to home page.
                            await this.Navigation.PopAsync();
                        }
                    };

                this.ToolbarItems.Add(deleteItem);
            }

            this.Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            // Attach handlers for Suspending and Resuming event.
            helper.Suspending += OnSuspending;
            helper.Resuming += OnResuming;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Only save it if there's some text somewhere.
            if (!String.IsNullOrWhiteSpace(note.Title) ||
                !String.IsNullOrWhiteSpace(note.Text))
            {
                note.SaveAsync();

                if (!isNoteEdit)
                {
                    App.NoteFolder.Notes.Add(note);
                }
            }

            // Detach handlers for Suspending and Resuming events.
            helper.Suspending -= OnSuspending;
            helper.Resuming -= OnResuming;

            base.OnDisappearing();
        }

        void OnSuspending()
        {
            // Save transient data and state.
            string str = note.Filename + "\x1F" +
                         isNoteEdit.ToString() + "\x1F" +
                         note.Title + "\x1F" +
                         note.Text;

            // Run entire WriteTextAsync in separate thread.
            Task task = Task.Run(() =>
                {
                    FileHelper.WriteTextAsync(App.TransientFilename, str);
                });

            // Wait for it to finish before finishing event handler.
            task.Wait();
        }

        async void OnResuming()
        {
            // Delete transient data and state.
            if (await FileHelper.ExistsAsync(App.TransientFilename))
            {
                await FileHelper.DeleteFileAsync(App.TransientFilename);
            }
        }
    }
}
