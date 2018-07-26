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
    public class UpdatedEntityLogRepository : GenericRepository<UpdatedEntityLog>, IUpdatedEntityLogRepository
    {
        public UpdatedEntityLogRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public List<UpdatedEntityLog> GetAllUpdatedEntityLogOfVolunteer(int volunteerId)
        {
            return FindBy(f => f.Entity_ID == volunteerId).ToList();
        }
    }
}
