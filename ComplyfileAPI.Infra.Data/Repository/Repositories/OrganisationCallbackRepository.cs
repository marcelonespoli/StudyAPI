using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationCallbackRepository : GenericRepository<OrganisationCallback>, IOrganisationCallbackRepository
    {
        public OrganisationCallbackRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public OrganisationCallback GetOrganisationCallback(int organisationId, string type)
        {
            return FindBy(f => f.Organisation_ID == organisationId && f.Type_VC == type).FirstOrDefault();
        }

        public async Task RunCallbackAsync(string json, string endPoint)
        {
            var parameter = new SqlParameter("@Body", json);
            var query = $"EXEC {endPoint} @Body";

            await Context.Database.ExecuteSqlCommandAsync(query, parameter);
        }
    }
}
