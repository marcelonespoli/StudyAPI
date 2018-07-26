using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.CrossCutting.Models;

namespace StudyAPI.Business.Interfaces
{
    public interface ITokenManager
    {
        string GenerateToken(GenerateTokenModel model);
        bool MakeTokenInvalid(Guid tokenId);
        bool DeleteAllVolunteerTokens(int volunteerId);
    }
}
