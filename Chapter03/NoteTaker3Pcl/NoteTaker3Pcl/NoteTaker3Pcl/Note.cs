namespace NoteTaker3Pcl
{
    class Note
    {
        public string Title { set; get; }

        public string Text { set; get; }

        public void Save(string filename)
        {
            string text = this.Title + "\n" + this.Text;
            FileHelper.WriteAllText(filename, text, () => { });
        }

        public void Load(string filename)
        {
            FileHelper.ReadAllText(filename, (string text) =>
                {
                    // Break string into Title and Text.
                    int index = text.IndexOf('\n');
                    this.Title = text.Substring(0, index);
                    this.Text = text.Substring(index + 1);
                });
        }
    }
}
