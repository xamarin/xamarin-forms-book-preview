using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using PlatformHelpers;

namespace NoteTaker8
{
    class NoteFolder
    {
        public NoteFolder()
        {
            this.Notes = new ObservableCollection<Note>();
            GetFilesAsync();
        }

        public ObservableCollection<Note> Notes { private set; get; }

        async void GetFilesAsync()
        {
            // Sort the filenames.
            IEnumerable<string> filenames =
                from filename in await FileHelper.GetFilesAsync()
                where filename.EndsWith(".note")
                orderby (filename)
                select filename;

            // Store them in the Notes collection.
            foreach (string filename in filenames)
            {
                Note note = new Note(filename);
                await note.LoadAsync();
                this.Notes.Add(note);
            }
        }
    }
}
