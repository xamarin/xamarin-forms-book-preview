using System;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker3Pcl.WinPhone.FileHelper))]

namespace NoteTaker3Pcl.WinPhone
{
    class FileHelper : IFileHelper
    {
        public void Exists(string filename, Action<bool> completed)
        {
            StorageFolder localFolder = 
                            ApplicationData.Current.LocalFolder;
            IAsyncOperation<StorageFile> createOp = 
                            localFolder.GetFileAsync(filename);
            createOp.Completed = (asyncInfo, asyncStatus) =>
            {
                completed(asyncStatus != AsyncStatus.Error);
            };
        }

        public void WriteAllText(string filename, string text, Action completed)
        {
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
                        completed();
                    };
                };
            };
        }

        public void ReadAllText(string filename, Action<string> completed)
        {
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
                        completed(text);
                    };
                };
            };
        }
    }
}
