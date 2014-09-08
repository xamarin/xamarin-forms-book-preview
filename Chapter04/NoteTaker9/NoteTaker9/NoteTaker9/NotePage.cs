using System;
using Xamarin.Forms;

using PlatformHelpers;

namespace NoteTaker9
{
    class NotePage : ContentPage
    {
        Note note;
        bool isNoteEdit;

        public NotePage(Note note, bool isNoteEdit = false)
        {
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
                // Cancel button.
                Button cancelButton = new Button
                {
                    Text = "Cancel",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                cancelButton.Clicked += async (sender, args) =>
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

                // Delete button
                Button deleteButton = new Button
                {
                    Text = "Delete",
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                deleteButton.Clicked += async (sender, args) =>
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

                StackLayout horzStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = 
                    {
                        cancelButton,
                        deleteButton
                    }
                };

                stackLayout.Children.Add(horzStack);
            }

            this.Content = stackLayout;
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
                note.SaveAsync();

                if (!isNoteEdit)
                {
                    App.NoteFolder.Notes.Add(note);
                }
            }

            base.OnDisappearing();
        }
    }
}
