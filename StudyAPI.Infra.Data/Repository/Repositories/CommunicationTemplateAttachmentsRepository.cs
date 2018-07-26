using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class CommunicationTemplateAttachmentsRepository : GenericRepository<CommunicationTemplateAttachments>, ICommunicationTemplateAttachmentsRepository
    {
        public CommunicationTemplateAttachmentsRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public List<CommunicationTemplateAttachments> GetCommunicationTemplateAttachmentList(int templateId)
        {
            return FindBy(f => f.Template_ID == templateId).ToList();
        }
    }
}
