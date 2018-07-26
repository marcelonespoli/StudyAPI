using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudyAPI.Infra.CrossCutting.Services.Interfaces
{
    public interface IBlobStorage
    {
        void SetBlobStorage(string containername);
        void SetBlobStorage(string containername, Stream filestream, string filepath, string mimetype);
        string GetBlobHtml(string htmlFile);
        byte[] GetBlobDocument(string blobNameForDocument);

    }
}
