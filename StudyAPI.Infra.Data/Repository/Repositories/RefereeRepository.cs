using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class RefereeRepository : GenericRepository<Referee>, IRefereeRepository
    {
        public RefereeRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public Referee GetReferee(int refereeId)
        {
            return FindBy(f => f.Referee_ID == refereeId).FirstOrDefault();
        }
    }
}
