using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.CrossCutting.Services.Interfaces;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.CrossCutting.Services.Document
{
    public class DocumentDownload : IDocumentDownload
    {
        public List<CommunicationTemplateAttachments> ListTemplateAttachments { get; set; }
        public List<Data.Entities.Document> ListDocumentsTemp { get; set; }

        private readonly IDocumentRepository _documentRepository;
        private readonly ICommunicationTemplateAttachmentsRepository _communicationTemplateAttachmentsRepository;
        private readonly IBlobStorage _blobStorage;

        public DocumentDownload(IDocumentRepository documentRepository, ICommunicationTemplateAttachmentsRepository communicationTemplateAttachmentsRepository, IBlobStorage blobStorage)
        {
            _documentRepository = documentRepository;
            _communicationTemplateAttachmentsRepository = communicationTemplateAttachmentsRepository;
            _blobStorage = blobStorage;
        }   

        public async Task<List<KeyValuePair<string, Stream>>> GetAttachmentsByTemplateId(int templateId, int organisationId)
        {
            var attachments = new List<KeyValuePair<string, Stream>>();
            var listDocuments = new List<Data.Entities.Document>();

            // Receive document types for templates 
            ListTemplateAttachments = _communicationTemplateAttachmentsRepository.GetCommunicationTemplateAttachmentList(templateId);

            if (ListTemplateAttachments.Count > 0)
            {
                foreach (var lta in ListTemplateAttachments)
                {
                    // Receive active document form the document type
                    ListDocumentsTemp = _documentRepository.GetDocumentToDownloadList(organisationId, lta.DocumentType_ID);

                    // Create a list with all documents from all documents type
                    foreach (var ld in ListDocumentsTemp)
                    {
                        listDocuments.Add(ld);
                    }
                }
            }
            else
            {
                return null;
            }

            // Access blob and download docs by listDocuments
            var container = "organisation-documents";
            _blobStorage.SetBlobStorage(container);

            foreach (var downloadFile in listDocuments)
            {
                byte[] doc = _blobStorage.GetBlobDocument(downloadFile.BlobName_VC);

                var stream = new MemoryStream(doc);
                attachments.Add(new KeyValuePair<string, System.IO.Stream>(downloadFile.DocumentName_VC, stream));
            }

            return attachments;
        }
    }
}
