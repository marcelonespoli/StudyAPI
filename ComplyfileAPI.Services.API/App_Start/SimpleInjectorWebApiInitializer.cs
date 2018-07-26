[assembly: WebActivator.PostApplicationStartMethod(typeof(ComplyfileAPI.Services.API.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace ComplyfileAPI.Services.API.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    using ComplyfileAPI.Infra.Data.Context;
    using ComplyfileAPI.Infra.Data.Repository.Interfaces;
    using ComplyfileAPI.Infra.Data.Repository.Repositories;
    using ComplyfileAPI.Infra.CrossCutting.Services.BlobStorage;
    using ComplyfileAPI.Infra.CrossCutting.Services.Document;
    using ComplyfileAPI.Infra.CrossCutting.Services.Email;
    using ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail;
    using ComplyfileAPI.Infra.CrossCutting.Services.Interfaces;
    using ComplyfileAPI.Business.Business;
    using ComplyfileAPI.Business.Interfaces;
    using ComplyfileAPI.Business.Validation;
    using ComplyfileAPI.Business.Validation.Interfaces;
    using ComplyfileAPI.Business.Validation.Specifications;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            // Business
            container.Register<IOrganisationManager, OrganisationManager>(Lifestyle.Scoped);
            container.Register<IVolunteerManager, VolunteerManager>(Lifestyle.Scoped);
            container.Register<ITokenManager, TokenManager>(Lifestyle.Scoped);

            // Validations
            container.Register<IValidation, Validation>(Lifestyle.Scoped);
            container.Register<OrganisationSpecifications>(Lifestyle.Scoped);
            container.Register<VolunteerSpecifications>(Lifestyle.Scoped);
            container.Register<TokenSpecification>(Lifestyle.Scoped);

            // Repository
            container.Register<ICommunicationTemplateAttachmentsRepository, CommunicationTemplateAttachmentsRepository>(Lifestyle.Scoped);
            container.Register<ICommunicationTemplateRepository, CommunicationTemplateRepository>(Lifestyle.Scoped);
            container.Register<IDocumentRepository, DocumentRepository>(Lifestyle.Scoped);
            container.Register<IEmailSentRepository, EmailSentRepository>(Lifestyle.Scoped);
            container.Register<IFinancialPlanRepository, FinancialPlanRepository>(Lifestyle.Scoped);
            container.Register<IInviteRepository, InviteRepository>(Lifestyle.Scoped);
            container.Register<IOrganisationCallbackRepository, OrganisationCallbackRepository>(Lifestyle.Scoped);
            container.Register<IOrganisationFinancialPlanRepository, OrganisationFinancialPlanRepository>(Lifestyle.Scoped);
            container.Register<IOrganisationRepository, OrganisationRepository>(Lifestyle.Scoped);
            container.Register<IOrganisationSettingsRepository, OrganisationSettingsRepository>(Lifestyle.Scoped);
            container.Register<IRefereeRepository, RefereeRepository>(Lifestyle.Scoped);
            container.Register<IUpdatedEntityLogRepository, UpdatedEntityLogRepository>(Lifestyle.Scoped);
            container.Register<IVolunteerRepository, VolunteerRepository>(Lifestyle.Scoped);
            container.Register<IVolunteerTokenRepository, VolunteerTokenRepository>(Lifestyle.Scoped);
            container.Register<ComplyfileApiContext>(Lifestyle.Scoped);

            // Services
            container.Register<IBlobStorage, BlobStorage>(Lifestyle.Scoped);
            container.Register<IDocumentDownload, DocumentDownload>(Lifestyle.Scoped);
            container.Register<IEmailFunctions, EmailFunctions>(Lifestyle.Scoped);
            container.Register<IMandrillEmailSender, MandrillEmailSender>(Lifestyle.Scoped);
        }
    }
}