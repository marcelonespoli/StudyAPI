using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class CommunicationTemplateRepository : GenericRepository<CommunicationTemplate>, ICommunicationTemplateRepository
    {
        public CommunicationTemplateRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public CommunicationTemplate GetCommunicationTemplate(string tamplateName, int organisationId, string entityType)
        {
            return FindBy(f => 
                    f.Name_VC.ToLower() == tamplateName &&
                    f.EntityType_VC == entityType &&
                    f.Organisation_ID == organisationId).FirstOrDefault();
        }

        public CommunicationTemplate GetCommunicationTemplateDefault(string tamplateName, string entityType)
        {
            return FindBy(f =>
                f.Name_VC.ToLower() == tamplateName &&
                f.EntityType_VC == entityType &&
                f.Organisation_ID == 0).FirstOrDefault();
        }
    }
}
