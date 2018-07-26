using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IVolunteerTokenRepository : IRepository<VolunteerToken>
    {
        bool SaveToken(VolunteerToken volunteerToken);
        bool InactiveToken(Guid tokenId);
        bool DeleteToken(Guid tokenId);
        bool DeleteAllVolunteerTokens(int volunteerId);
        bool IsTokenActive(VolunteerToken volunteerToken);
    }
}
