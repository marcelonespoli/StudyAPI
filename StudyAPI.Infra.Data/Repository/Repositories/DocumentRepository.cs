using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public List<Document> GetDocumentToDownloadList(int organisationId, int documentTypeId)
        {
            var currentDate = DateTime.Now.Date;

            return (from d in Context.Document
                    where d.Organisation_ID == organisationId
                    where d.Volunteer_ID == 0
                    where d.Visibility_IN == 3 // -> Administrator & All Volunteers
                    where d.DocumentType_ID == documentTypeId &&
                          (d.ExpiryDate_DT >= currentDate ||
                           d.ExpiryDate_DT == null)
                    select d).ToList();
        }
    }
}
