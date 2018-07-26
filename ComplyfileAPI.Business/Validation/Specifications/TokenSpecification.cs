using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Business.Validation.Specifications
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
