using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface ICommunicationTemplateAttachmentsRepository : IRepository<CommunicationTemplateAttachments>
    {
        List<CommunicationTemplateAttachments> GetCommunicationTemplateAttachmentList(int templateId);
    }
}
