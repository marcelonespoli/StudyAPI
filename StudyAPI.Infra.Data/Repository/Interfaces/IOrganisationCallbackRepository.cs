using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationCallbackRepository : IRepository<OrganisationCallback>
    {
        OrganisationCallback GetOrganisationCallback(int organisationId, string type);
        Task RunCallbackAsync(string json, string endPoint);
    }
}
