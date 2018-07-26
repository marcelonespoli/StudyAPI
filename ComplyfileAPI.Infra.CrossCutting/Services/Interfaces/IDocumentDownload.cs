using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ComplyfileAPI.Infra.CrossCutting.Services.Interfaces
{
    public interface IDocumentDownload
    {
        Task<List<KeyValuePair<string, Stream>>> GetAttachmentsByTemplateId(int templateId, int organisationId);
    }
}
