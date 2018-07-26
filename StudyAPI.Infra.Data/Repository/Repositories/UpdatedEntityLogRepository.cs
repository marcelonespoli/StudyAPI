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
    public class UpdatedEntityLogRepository : GenericRepository<UpdatedEntityLog>, IUpdatedEntityLogRepository
    {
        public UpdatedEntityLogRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public List<UpdatedEntityLog> GetAllUpdatedEntityLogOfVolunteer(int volunteerId)
        {
            return FindBy(f => f.Entity_ID == volunteerId).ToList();
        }
    }
}
