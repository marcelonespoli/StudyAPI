using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface ICommunicationTemplateRepository : IRepository<CommunicationTemplate>
    {
        CommunicationTemplate GetCommunicationTemplate(string tamplateName, int organisationId, string entityType);
        CommunicationTemplate GetCommunicationTemplateDefault(string tamplateName, string entityType);
    }
}
