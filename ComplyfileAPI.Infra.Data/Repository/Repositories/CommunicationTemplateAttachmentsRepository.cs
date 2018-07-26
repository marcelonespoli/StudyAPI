using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class CommunicationTemplateAttachmentsRepository : GenericRepository<CommunicationTemplateAttachments>, ICommunicationTemplateAttachmentsRepository
    {
        public CommunicationTemplateAttachmentsRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public List<CommunicationTemplateAttachments> GetCommunicationTemplateAttachmentList(int templateId)
        {
            return FindBy(f => f.Template_ID == templateId).ToList();
        }
    }
}
