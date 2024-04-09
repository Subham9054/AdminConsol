using System.Collections.Generic;
using ClaimBaseTest.Repository.Factory;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using ClaimBaseTest.Repository;

using ClaimBaseTest.Model.City;
using Dapper;
using System.Data;
namespace ClaimBaseTest.Repository.Repository
{
	public class CityMasterRepository:WizTestRepositoryBase,ICityMasterRepository
	{
		public CityMasterRepository(IWizTestConnectionFactory WizTestConnectionFactory) : base(WizTestConnectionFactory)
        {

        }
		 public async Task<int> Insert_City(City TBL)
	{
		var p = new DynamicParameters();
		if(TBL.CityId == 0 )
		{
			p.Add("@P_Action", "I");
			p.Add("@P_CityId",0);
		}
		else
		{
			p.Add("@P_Action", "U");
			p.Add("@P_CityId",TBL.CityId);
		}
		p.Add("@P_CityName",TBL.CityName);
		var results = await Connection.ExecuteAsync("USP_City_Copy_2", p, commandType: CommandType.StoredProcedure);		
return 1;

		} 
	public async Task<int> Delete_City(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "D");
			p.Add("@P_CityId",Id);
		
		var results = await Connection.ExecuteAsync("USP_City_Copy_2", p, commandType: CommandType.StoredProcedure);
		return results;

		} 
	public async Task<City> GetCityById(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "E");
			p.Add("@P_CityId",Id);
		
		var results = await Connection.QueryAsync<City>("USP_City_Copy_2", p, commandType: CommandType.StoredProcedure);
		return results.FirstOrDefault();

		} 
	public async Task<List<City>> Getall_City(City TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "V");
		var results = await Connection.QueryAsync<City>("USP_City_Copy_2",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	public async Task<List<City>> Search_City(City TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "S");
			p.Add("@P_CityId",TBL.CityId);
		
		p.Add("@P_CityName",TBL.CityName);
		var results = await Connection.QueryAsync<City>("USP_City_Copy_2",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	
}
}
