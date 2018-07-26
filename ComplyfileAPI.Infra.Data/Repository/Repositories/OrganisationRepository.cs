using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationRepository : GenericRepository<Organisation>, IOrganisationRepository
    {
        public OrganisationRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public Organisation GetOrganisation(int organisationId)
        {
            return FindBy(o => o.Organisation_ID == organisationId).FirstOrDefault();
        }

        public DateTime? GetTrialExpiresDate(int organisationId)
        {
            return  FindBy(o => o.Organisation_ID == organisationId).FirstOrDefault()?.TrialExpires_DT;
        }

        public List<DisplaySelectOrganisation> GetAllOrganisationByVolunteerEmail(string email)
        {
            return (from o in Context.Organisation
                    from v in Context.Volunteer
                    where v.Organisation_ID == o.Organisation_ID
                    where v.Email_VC == email
                    select new DisplaySelectOrganisation
                    {
                        OrganisationId = o.Organisation_ID,
                        Name = o.Name_VC
                    }).OrderBy(o => o.Name).ToList();
        }
        
    }
}
