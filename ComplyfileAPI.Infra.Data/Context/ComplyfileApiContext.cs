using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Context
{
    public class ComplyfileApiContext : DbContext
    {
        public ComplyfileApiContext()
            :base("ApiContext")
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            if (objectContext != null)
                objectContext.CommandTimeout = 200;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<ComplyfileApiContext>(null);
        }

        public DbSet<Color> Color { get; set; }
        public DbSet<CommunicationTemplate> CommunicationTemplate { get; set; }
        public DbSet<CommunicationTemplateAttachments> CommunicationTemplateAttachments { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<EmailSent> EmailSent { get; set; }
        public DbSet<EmailSentStatus> EmailSentStatus { get; set; }
        public DbSet<FinancialPlan> FinancialPlan { get; set; }
        public DbSet<Invite> Invite { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<OrganisationCallback> OrganisationCallback { get; set; }
        public DbSet<OrganisationFinancialPlan> OrganisationFinancialPlan { get; set; }
        public DbSet<OrganisationSettings> OrganisationSettings { get; set; }
        public DbSet<Referee> Referee { get; set; }
        public DbSet<Signatory> Signatory { get; set; }
        public DbSet<UpdatedEntityLog> UpdatedEntityLog { get; set; }
        public DbSet<Volunteer> Volunteer { get; set; }
        public DbSet<VolunteerStatus> VolunteerStatus { get; set; }
        public DbSet<VolunteerToken> VolunteerToken { get; set; }
    }
}
