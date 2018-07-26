using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface ICommunicationTemplateAttachmentsRepository : IRepository<CommunicationTemplateAttachments>
    {
        List<CommunicationTemplateAttachments> GetCommunicationTemplateAttachmentList(int templateId);
    }
}
