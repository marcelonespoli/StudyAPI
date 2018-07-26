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
using ComplyfileAPI.Infra.CrossCutting.Services.Interfaces;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.CrossCutting.Services.BlobStorage
{

    public class BlobStorage : IBlobStorage
    {
        private const string AccountKey = "vFbFhDMQgKvq/WHCjy3asbVIDkqkvaKvj80LKPh5Eo0Lw0K9xFTAw1rCf2IdNbcV5JqW9U4U9cyEwpCz8iRLjA==";
        private const string AccountName = "complyfilestorage";
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

        //public void CopyBlobToContainer(string blobName, string newBlobName, string sourcecontainer, string destinationcontainer)
        //{
        //    CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(AccountName, AccountKey), true);
        //    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer sourceContainer = cloudBlobClient.GetContainerReference(sourcecontainer);
        //    CloudBlobContainer targetContainer = cloudBlobClient.GetContainerReference(destinationcontainer);

        //    CloudBlockBlob sourceBlob = sourceContainer.GetBlockBlobReference(blobName);
        //    CloudBlockBlob targetBlob = targetContainer.GetBlockBlobReference(blobName);
        //    //targetBlob.StartCopy(sourceBlob);
        //    targetBlob.StartCopyAsync(sourceBlob);

        //    // now to remame it with unique reference
        //    RenameFile(blobName, newBlobName, destinationcontainer);
        //}

        //public void RenameFile(string blobName, string newBlobName, string destinationcontainer)
        //{
        //    CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(AccountName, AccountKey), true);
        //    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer targetContainer = cloudBlobClient.GetContainerReference(destinationcontainer);

        //    // now to remame it with unique reference
        //    CloudBlockBlob existBlob = targetContainer.GetBlockBlobReference(blobName);
        //    CloudBlockBlob newBlob = targetContainer.GetBlockBlobReference(newBlobName);
        //    //create a new blob
        //    //newBlob.StartCopy(existBlob);
        //    newBlob.StartCopyAsync(existBlob);
        //    //delete the old
        //    //existBlob.Delete();
        //    existBlob.DeleteAsync();
        //}

        //public Image getBlobImage(string imageName)
        //{
        //    string baseUri = null;
        //    CloudBlobClient blobStorage = null;
        //    CloudBlockBlob blob = null;
        //    Image img = null;

        //    try
        //    {
        //        byte[] key = Convert.FromBase64String(AccountKey);
        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);

        //        baseUri = string.Format("http://{0}.blob.core.windows.net", AccountName);
        //        blobStorage = creds.CreateCloudBlobClient();

        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);
        //        blob = blobContainer.GetBlockBlobReference(imageName);

        //        MemoryStream blobStream = new MemoryStream();
        //        blob.DownloadToStream(blobStream);
        //        img = Image.FromStream(blobStream);

        //        return img;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error getting blob image from storage: " + imageName, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        return img;
        //    }
        //}

        //public string getBlobAsBase64(string imageName)
        //{
        //    string result = string.Empty;
        //    string baseUri = null;
        //    CloudBlobClient blobStorage = null;
        //    CloudBlockBlob blob = null;
        //    Image img = null;

        //    try
        //    {
        //        byte[] key = Convert.FromBase64String(AccountKey);
        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);

        //        baseUri = string.Format("http://{0}.blob.core.windows.net", AccountName);
        //        blobStorage = creds.CreateCloudBlobClient();

        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);
        //        blob = blobContainer.GetBlockBlobReference(imageName);
        //        MemoryStream blobStream = new MemoryStream();
        //        blob.DownloadToStream(blobStream);
        //        img = Image.FromStream(blobStream);

        //        try
        //        {
        //            byte[] blobFile = blobStream.ToArray();
        //            result = Convert.ToBase64String(blobFile);
        //            var extension = System.IO.Path.GetExtension(imageName);
        //            result = "data:" + MimeMapping.GetMimeMapping(extension) + ";base64," + result;
        //        }
        //        finally
        //        {
        //            blobStream.Close();
        //            blobStream.Dispose();
        //            blobStream = null;
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error getting blob image from storage: " + imageName, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        return result;
        //    }
        //}

        //public List<string> getBlob()
        //{
        //    string baseUri = null;
        //    CloudBlobClient blobStorage = null;
        //    IEnumerable<IListBlobItem> blobs = null;
        //    List<string> temp = new List<string>();

        //    try
        //    {
        //        byte[] key = Convert.FromBase64String(AccountKey);
        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);
        //        baseUri = string.Format("http://{0}.blob.core.windows.net", AccountName);
        //        blobStorage = creds.CreateCloudBlobClient();
        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);
        //        blobs = blobContainer.ListBlobs();

        //        foreach (IListBlobItem blob1 in blobs)
        //        {
        //            temp.Add(blob1.Uri.Segments[2].ToString());
        //        }

        //        return temp;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error getting blob image from storage.", Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        return temp;
        //    }
        //}

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
                //Utilities.LogError("Error getting blob file from storage: " + htmlFile, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
                return html;
            }
        }

        //public byte[] getBlobBytes(string htmlFile)
        //{
        //    CloudBlockBlob blob = null;

        //    try
        //    {
        //        byte[] key = Convert.FromBase64String(AccountKey);

        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);
        //        CloudBlobClient blobStorage = creds.CreateCloudBlobClient();
        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);

        //        blob = blobContainer.GetBlockBlobReference(htmlFile);
        //        MemoryStream blobStream = new MemoryStream();
        //        blob.DownloadToStream(blobStream);

        //        return blobStream.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error getting blob Bytes from storage: " + htmlFile, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        return null;
        //    }
        //}

        //public void DeleteFile()
        //{
        //    try
        //    {
        //        string baseUri = null;

        //        CloudBlobClient blobStorage = null;
        //        byte[] key = Convert.FromBase64String(AccountKey);

        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);
        //        baseUri = string.Format("http://{0}.blob.core.windows.net", AccountName);
        //        blobStorage = creds.CreateCloudBlobClient();

        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);
        //        var perms = new BlobContainerPermissions

        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Container
        //        };

        //        // This line makes the blob public so it is available from a web browser (no magic needed to read it)
        //        blobContainer.SetPermissions(perms);
        //        var fi = new FileInfo(FilePath);

        //        // delete the file
        //        string blobUriPath = fi.Name;
        //        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobUriPath);
        //        blob.DeleteIfExists();
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error deleting blob from storage", Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public CloudBlockBlob UploadFile()
        //{
        //    try
        //    {
        //        string baseUri = null;
        //        CloudBlobClient blobStorage = null;
        //        byte[] key = Convert.FromBase64String(AccountKey);

        //        CloudStorageAccount creds = new CloudStorageAccount(new StorageCredentials(AccountName, key), true);
        //        baseUri = string.Format("http://{0}.blob.core.windows.net", AccountName);
        //        blobStorage = creds.CreateCloudBlobClient();
        //        CloudBlobContainer blobContainer = blobStorage.GetContainerReference(ContainerName);

        //        bool didNotExistCreated = blobContainer.CreateIfNotExists();

        //        //  and has permissions Read, Write, and List 
        //          SharedAccessBlobPolicy storedPolicy = new SharedAccessBlobPolicy()
        //          {
        //            SharedAccessExpiryTime = DateTime.UtcNow.AddHours(10),
        //            Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Delete,
        //          };


        //        BlobContainerPermissions permissions = new BlobContainerPermissions();

        //        permissions.SharedAccessPolicies.Clear();
        //        permissions.SharedAccessPolicies.Add("ComplyfilePolicy", storedPolicy);  


        //        // This line makes the blob public so it is available from a web browser (no magic needed to read it)
        //        blobContainer.SetPermissions(permissions);

        //        var fi = new FileInfo(FilePath);

        //        // upload the file from stream
        //        string blobUriPath = fi.Name;
        //        CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobUriPath);
        //        blob.UploadFromStream(InputStream);
        //        blob.Properties.ContentType = MimeTypeName;

        //        // REST call under the hood                       
        //        blob.SetProperties();

        //        // not required – just showing how to store metadata
        //        blob.Metadata["SourceFileName"] = fi.FullName;
        //        blob.Metadata["WhenFileUploadedUtc"] = DateTime.UtcNow.ToLongTimeString();

        //        // REST call under the hood
        //        try
        //        {
        //            blob.SetMetadata();
        //        }
        //        catch (Exception ex)
        //        {
        //            // sometimes fails but do we care?
        //        }

        //        return blob;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.LogError("Error upoading a file to storage", Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
        //        throw new Exception(ex.Message);
        //    }
        //}

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
                //Utilities.LogError("Error getting blob file from storage: " + blobNameForDocument, Utilities.ErrorArea.BlobStorage, Utilities.ErrorPriority.Low, false, ex);
                return null;
            }
        }

        //public string GetSasForBlobUsingAccessPolicy(CloudBlockBlob cloudBlockBlob)
        //{
        //    //call to set the shared access policy on the container
        //    //in the real world, this would be passed in, not hardcoded!
        //    string sharedAccessPolicyName = "ComplyfilePolicy";


        //    //using that shared access policy, get the sas token and set the url
        //    string sasToken = cloudBlockBlob.GetSharedAccessSignature(null, sharedAccessPolicyName);
        //    return string.Format(CultureInfo.InvariantCulture, "{0}{1}", cloudBlockBlob.Uri, sasToken);
        //}

    }
}