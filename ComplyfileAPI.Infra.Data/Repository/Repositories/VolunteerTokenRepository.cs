using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class VolunteerTokenRepository : GenericRepository<VolunteerToken>, IVolunteerTokenRepository
    {
        public VolunteerTokenRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public bool SaveToken(VolunteerToken volunteerToken)
        {
            if (volunteerToken != null)
            {
                Insert(volunteerToken);
                Save();
                return true;
            }

            return false;
        }

        public bool InactiveToken(Guid tokenId)
        {
            var volunteerToken = FindBy(t => t.Id == tokenId).FirstOrDefault();
            
            if (volunteerToken != null)
            {
                volunteerToken.Active_BT = false;
                Update(volunteerToken);
                Save();
                return true;
            }

            return false;
        }

        public bool DeleteToken(Guid tokenId)
        {
            var volunteerToken = FindBy(t => t.Id == tokenId).FirstOrDefault();

            if (volunteerToken != null)
            {
                Delete(volunteerToken);
                Save();
                return true;
            }

            return false;
        }

        public bool DeleteAllVolunteerTokens(int volunteerId)
        {
            var allVolunteerTokens = FindBy(t => t.Volunteer_ID == volunteerId).ToList();

            if (allVolunteerTokens.Any())
            {
                Context.VolunteerToken.RemoveRange(allVolunteerTokens);
                Save();
                return true;
            }

            return false;
        }

        public bool IsTokenActive(VolunteerToken volunteerToken)
        {
            return Context.VolunteerToken.Any(t => t.Volunteer_ID == volunteerToken.Volunteer_ID &&
                                                   t.Token_VC == volunteerToken.Token_VC &&
                                                   t.Id == volunteerToken.Id &&
                                                   t.Active_BT);
        }

    }
}
