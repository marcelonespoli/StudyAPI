using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class RefereeRepository : GenericRepository<Referee>, IRefereeRepository
    {
        public RefereeRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public Referee GetReferee(int refereeId)
        {
            return FindBy(f => f.Referee_ID == refereeId).FirstOrDefault();
        }
    }
}
