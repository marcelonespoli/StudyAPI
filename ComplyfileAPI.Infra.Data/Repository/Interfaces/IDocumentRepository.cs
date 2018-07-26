using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        List<Document> GetDocumentToDownloadList(int organisationId, int documentTypeId);
    }
}
