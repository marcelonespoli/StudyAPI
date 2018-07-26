﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationFinancialPlanRepository : GenericRepository<OrganisationFinancialPlan>, IOrganisationFinancialPlanRepository
    {
        public OrganisationFinancialPlanRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public OrganisationFinancialPlan GetOrganisationFinancialPlan(int organisationId)
        {
            return FindBy(o => o.Organisation_ID == organisationId).FirstOrDefault();
        }

        public int? GetMaxVolunteersOfOrganisationFinancialPlan(int organisationId)
        {
            return FindBy(f => f.Organisation_ID == organisationId).FirstOrDefault()?.MaxVolunteers_IN;
        }
    }
}
