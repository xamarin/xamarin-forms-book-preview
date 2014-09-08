using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Xamarin.Forms;

[assembly: Dependency(typeof(NoteTaker7.WinPhone.FileHelper))]

namespace NoteTaker7.WinPhone
{
    class FileHelper : IFileHelper
    {
        public async Task<bool> ExistsAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            try
            {
                await localFolder.GetFileAsync(filename);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task WriteTextAsync(string filename, string text)
        {
            StorageFolder localFolder = 
                    ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = 
                    await localFolder.CreateFileAsync(filename,
                                CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream stream = 
                    await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter dataWriter = new DataWriter(stream))
                {
                    dataWriter.WriteString(text);
                    await dataWriter.StoreAsync();
                }
            }
        }

        public async Task<string> ReadTextAsync(string filename)
        {
            StorageFolder localFolder = 
                    ApplicationData.Current.LocalFolder;
            IStorageFile storageFile = 
                    await localFolder.GetFileAsync(filename);
            using (IRandomAccessStream stream = 
                    await storageFile.OpenReadAsync())
            {
                using (DataReader dataReader = new DataReader(stream))
                {
                    uint length = (uint)stream.Size;
                    await dataReader.LoadAsync(length);
                    return dataReader.ReadString(length);
                }
            }
        }
    }
}
