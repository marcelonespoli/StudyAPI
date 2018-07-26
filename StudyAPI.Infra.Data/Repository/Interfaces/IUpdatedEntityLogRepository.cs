using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IUpdatedEntityLogRepository : IRepository<UpdatedEntityLog>
    {
        List<UpdatedEntityLog> GetAllUpdatedEntityLogOfVolunteer(int volunteerId);
    }
}
