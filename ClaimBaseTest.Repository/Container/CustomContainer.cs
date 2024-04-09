using  Microsoft.Extensions.DependencyInjection;
using  Microsoft.Extensions.Configuration;

using ClaimBaseTest.Repository.Factory;
using ClaimBaseTest.Repository.Repository;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Repository.Repository;
namespace ClaimBaseTest.Repository.Container
{
	public static class CustomContainer
	{
		public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
		{
		IWizTestConnectionFactory WizTestconnectionFactory=new WizTestConnectionFactory(configuration.GetConnectionString("DBconnectionWizTest"));
		services.AddSingleton<IWizTestConnectionFactory> (WizTestconnectionFactory);

		services.AddScoped<ICityMasterRepository, CityMasterRepository>();
		services.AddScoped<ICodeGenLoginRepository, CodeGenLoginRepository>();
		services.AddScoped<ISendSMSRepository, SendSMSRepository>();
		services.AddScoped<ICompanyMasterRepository, CompanyMasterRepository>();
		services.AddScoped<IEmpMasterRepository, EmpMasterRepository>();
		services.AddScoped<IHierarchyServiceProviderRepository, HierarchyServiceProviderRepository>();
		services.AddScoped<ILevelServiceProvider, LevelServiceProvider>();
		services.AddScoped<ILevelDetailsServiceProvider, LeveDetailslServiceProvider>();
		services.AddScoped<IDesignationServiceProvider, DesignationServiceProvider>();
		services.AddScoped<IFuncServiceProvider, FuncServiceProviderNew>();
		services.AddScoped<IProjectLinkServiceProvider, ProjectLinkServiceProvider>();
		services.AddScoped<IGblLinkServiceProvider, GblLinkServiceProvider>();
		services.AddScoped<IPriLinkServiceProvider, PriLinkServiceProvider>();
		services.AddScoped<ISetPermissionServiceProvider, SetPermissionServiceProvider>();
		services.AddScoped<IUserServiceProvider, UserServiceProvider>();
        services.AddScoped<IMenueManageProvider, MenueManageServiceProvider>();
            //Write code here
        }
	}
}
