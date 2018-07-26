using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IRefereeRepository : IRepository<Referee>
    {
        Referee GetReferee(int refereeId);
    }
}
