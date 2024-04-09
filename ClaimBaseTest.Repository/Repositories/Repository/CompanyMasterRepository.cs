using System.Collections.Generic;
using ClaimBaseTest.Repository.Factory;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using ClaimBaseTest.Repository;

using ClaimBaseTest.Model.Company;
using Dapper;
using System.Data;
namespace ClaimBaseTest.Repository.Repository
{
	public class CompanyMasterRepository:WizTestRepositoryBase,ICompanyMasterRepository
	{
		public CompanyMasterRepository(IWizTestConnectionFactory WizTestConnectionFactory) : base(WizTestConnectionFactory)
        {

        }
		 public async Task<int> Insert_Company(Company TBL)
	{
		var p = new DynamicParameters();
		if(TBL.CompanyID == 0 )
		{
			p.Add("@P_Action", "I");
			p.Add("@P_CompanyID",0);
		}
		else
		{
			p.Add("@P_Action", "U");
			p.Add("@P_CompanyID",TBL.CompanyID);
		}
		p.Add("@P_ComanyName",TBL.ComanyName);
		p.Add("@P_CompanyAddress",TBL.CompanyAddress);
		p.Add("@P_CompanyBranch",TBL.CompanyBranch);
		p.Add("@P_CompanyDocument",TBL.CompanyDocument);
		var results = await Connection.ExecuteAsync("USP_Company_Copy_3", p, commandType: CommandType.StoredProcedure);		
return 1;

		} 
	public async Task<int> Delete_Company(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "D");
			p.Add("@P_CompanyID",Id);
		
		var results = await Connection.ExecuteAsync("USP_Company_Copy_3", p, commandType: CommandType.StoredProcedure);
		return results;

		} 
	public async Task<Company> GetCompanyById(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "E");
			p.Add("@P_CompanyID",Id);
		
		var results = await Connection.QueryAsync<Company>("USP_Company_Copy_3", p, commandType: CommandType.StoredProcedure);
		return results.FirstOrDefault();

		} 
	public async Task<List<Company>> Getall_Company(Company TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "V");
		var results = await Connection.QueryAsync<Company>("USP_Company_Copy_3",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	public async Task<List<Company>> Search_Company(Company TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "S");
			p.Add("@P_CompanyID",TBL.CompanyID);
		
		p.Add("@P_ComanyName",TBL.ComanyName);
		p.Add("@P_CompanyAddress",TBL.CompanyAddress);
		p.Add("@P_CompanyBranch",TBL.CompanyBranch);
		p.Add("@P_CompanyDocument",TBL.CompanyDocument);
		var results = await Connection.QueryAsync<Company>("USP_Company_Copy_3",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	
}
}
