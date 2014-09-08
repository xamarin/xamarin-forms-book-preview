using System;

#if !WINDOWS_PHONE
using System.IO;
#else
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace NoteTaker3Sap
{
    class Note
    {
        public string Title { set; get; }

        public string Text { set; get; }

        public void Save(string filename)
        {
            string text = this.Title + "\n" + this.Text;

#if !WINDOWS_PHONE // iOS and Android

            string docsPath = Environment.GetFolderPath(
                                    Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(docsPath, filename);
            File.WriteAllText(filepath, text);

#else // Windows Phone

            StorageFolder localFolder = 
                                ApplicationData.Current.LocalFolder;
            IAsyncOperation<StorageFile> createOp = 
                                localFolder.CreateFileAsync(filename, 
                                    CreationCollisionOption.ReplaceExisting);

            createOp.Completed = (asyncInfo1, asyncStatus1) =>
            {
                IStorageFile storageFile = asyncInfo1.GetResults();
                IAsyncOperation<IRandomAccessStream> openOp = 
                        storageFile.OpenAsync(FileAccessMode.ReadWrite);
                openOp.Completed = (asyncInfo2, asyncStatus2) =>
                {
                    IRandomAccessStream stream = asyncInfo2.GetResults();
                    DataWriter dataWriter = new DataWriter(stream);
                    dataWriter.WriteString(text);
                    DataWriterStoreOperation storeOp = 
                            dataWriter.StoreAsync();
                    storeOp.Completed = (asyncInfo3, asyncStatus3) =>
                    {
                        dataWriter.Dispose();
                    };
                };
            };
     
#endif
        }

        public void Load(string filename)
        {

#if !WINDOWS_PHONE // iOS and Android

            string docsPath = Environment.GetFolderPath(
                                    Environment.SpecialFolder.Personal);
            string filepath = Path.Combine(docsPath, filename);
            string text = File.ReadAllText(filepath);

            // Break string into Title and Text.
            int index = text.IndexOf('\n');
            this.Title = text.Substring(0, index);
            this.Text = text.Substring(index + 1);

#else // Windows Phone

            StorageFolder localFolder = 
                                ApplicationData.Current.LocalFolder;
            IAsyncOperation<StorageFile> createOp = 
                                localFolder.GetFileAsync(filename);
            createOp.Completed = (asyncInfo1, asyncStatus1) =>
            {
                IStorageFile storageFile = asyncInfo1.GetResults();
                IAsyncOperation<IRandomAccessStreamWithContentType> 
                        openOp = storageFile.OpenReadAsync();
                openOp.Completed = (asyncInfo2, asyncStatus2) =>
                {
                    IRandomAccessStream stream = asyncInfo2.GetResults();
                    DataReader dataReader = new DataReader(stream);
                    uint length = (uint)stream.Size;
                    DataReaderLoadOperation loadOp = 
                            dataReader.LoadAsync(length);
                    loadOp.Completed = (asyncInfo3, asyncStatus3) =>
                    {
                        string text = dataReader.ReadString(length);
                        dataReader.Dispose();

                        // Break string into Title and Text.
                        int index = text.IndexOf('\n');
                        this.Title = text.Substring(0, index);
                        this.Text = text.Substring(index + 1);
                    };
                };
            };
     
#endif

        }
    }
}
