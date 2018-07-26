using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Business.Validation.Specifications
{
    public class TokenSpecification
    {
        private readonly IVolunteerTokenRepository _volunteerTokenRepository;

        public TokenSpecification(IVolunteerTokenRepository volunteerTokenRepository)
        {
            _volunteerTokenRepository = volunteerTokenRepository;
        }

        public bool IsTokenActive(VolunteerToken volunteerToken)
        {
            return _volunteerTokenRepository.IsTokenActive(volunteerToken);
        }
    }
}
