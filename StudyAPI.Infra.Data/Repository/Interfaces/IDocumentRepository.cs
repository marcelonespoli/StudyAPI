using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        List<Document> GetDocumentToDownloadList(int organisationId, int documentTypeId);
    }
}
