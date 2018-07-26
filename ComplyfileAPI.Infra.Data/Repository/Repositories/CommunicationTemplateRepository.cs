using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class CommunicationTemplateRepository : GenericRepository<CommunicationTemplate>, ICommunicationTemplateRepository
    {
        public CommunicationTemplateRepository(ComplyfileApiContext context) 
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
