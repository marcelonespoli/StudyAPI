using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using System.Globalization;
using System.Threading.Tasks;
using StudyAPI.Infra.CrossCutting.Services.Interfaces;
using StudyAPI.Infra.Data.Repository.Interfaces;
using StudyAPI.Infra.CrossCutting.Helpers;

namespace StudyAPI.Infra.CrossCutting.Services.BlobStorage
{

    public class BlobStorage : IBlobStorage
    {
        private const string AccountKey = "xxx=";
        private const string AccountName = "xxx";
        private string ContainerName = "";
        private Stream InputStream = null;
        private string MimeTypeName = "";
        private string FilePath = "";
        
        public string GetBlobBaseURL()
        {
            return String.Format("http://{0}.blob.core.windows.net/{1}/", AccountName, ContainerName);
        }

        public void SetBlobStorage(string containername, Stream filestream, string filepath, string mimetype)
        {
            // pass in the single snippet code file you want uploaded
            InputStream = filestream;
            ContainerName = containername;
            MimeTypeName = mimetype;
            FilePath = filepath;
        }

        public void SetBlobStorage(string containername)
        {
            // pass in the container to download from
            ContainerName = containername;
        }

        public string GetBlobHtml(string htmlFile)
        {
            var html = "";

            try
            {
                byte[] key = Convert.FromBase64String(AccountKey);
                CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);
                CloudBlobClient blobStorage = creds.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);

                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(htmlFile);

                MemoryStream blobStream = new MemoryStream();
                blob.DownloadToStream(blobStream);
                blobStream.Position = 0;

                StreamReader streamReader = new StreamReader(blobStream);
                html = streamReader.ReadToEnd();

                return html;
            }
            catch (Exception ex)
            {
                Utilities.LogError("Error getting blob file from storage: " + htmlFile, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
                return html;
            }
        }

        public byte[] GetBlobDocument(string blobNameForDocument)
        {
            CloudBlockBlob blob = null;

            try
            {
                byte[] key = Convert.FromBase64String(AccountKey);
                CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);

                CloudBlobClient blobStorage = creds.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);
                blob = blobContainer.GetBlockBlobReference(blobNameForDocument);

                MemoryStream blobStream = new MemoryStream();
                blob.DownloadToStream(blobStream);
                
                byte[] result = blobStream.ToArray();

                return result;
            }
            catch (Exception ex)
            {
                Utilities.LogError("Error getting blob file from storage: " + blobNameForDocument, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
                return null;
            }
        }


    }
}