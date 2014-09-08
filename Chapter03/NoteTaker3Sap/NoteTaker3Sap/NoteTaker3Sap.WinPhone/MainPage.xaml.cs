using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;





using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;







namespace NoteTaker3Sap.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();


            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            IAsyncOperation<StorageFile> createOp = 
                                storageFolder.CreateFileAsync("filename");
            createOp.Completed = OnCreateFileCompleted;




            Forms.Init();
            Content = NoteTaker3Sap.App.GetMainPage().ConvertPageToUIElement(this);
        }

        void OnCreateFileCompleted(IAsyncOperation<StorageFile> createOp, 
                                   AsyncStatus asyncStatus)
        {
            if (asyncStatus == AsyncStatus.Completed)
            {
                StorageFile storageFile = createOp.GetResults();

                IAsyncOperation<IRandomAccessStream> openOp = 
                            storageFile.OpenAsync(FileAccessMode.ReadWrite);
                openOp.Completed = OnOpenFileCompleted;
            }
            else
            {
                // deal with cancellation or error
            }
        }

        void OnOpenFileCompleted(IAsyncOperation<IRandomAccessStream> openOp, 
                                 AsyncStatus asyncStatus)
        {
            // ...
        }
    }
}
