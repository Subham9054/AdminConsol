using System.Collections.Generic;
using ClaimBaseTest.Repository.Factory;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Repositories.Interfaces;
using ClaimBaseTest.Repository;

using ClaimBaseTest.Model.Employee;
using Dapper;
using System.Data;
namespace ClaimBaseTest.Repository.Repository
{
	public class EmpMasterRepository:WizTestRepositoryBase,IEmpMasterRepository
	{
		public EmpMasterRepository(IWizTestConnectionFactory WizTestConnectionFactory) : base(WizTestConnectionFactory)
        {

        }
		 public async Task<int> Insert_Employee(Employee TBL)
	{
		var p = new DynamicParameters();
		if(TBL.EmployeeID == 0 )
		{
			p.Add("@P_Action", "I");
			p.Add("@P_EmployeeID",0);
		}
		else
		{
			p.Add("@P_Action", "U");
			p.Add("@P_EmployeeID",TBL.EmployeeID);
		}
		p.Add("@P_LastName",TBL.LastName);
		p.Add("@P_FirstName",TBL.FirstName);
		p.Add("@P_Qualification",TBL.Qualification);
		p.Add("@P_Address",TBL.Address);
		p.Add("@P_City",TBL.City);
		p.Add("@P_Gender",TBL.Gender);
		p.Add("@P_EmployeeDocument",TBL.EmployeeDocument);
		var results = await Connection.ExecuteAsync("USP_Employee_Copy_2", p, commandType: CommandType.StoredProcedure);		
return 1;

		} 
	public async Task<int> Delete_Employee(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "D");
			p.Add("@P_EmployeeID",Id);
		
		var results = await Connection.ExecuteAsync("USP_Employee_Copy_2", p, commandType: CommandType.StoredProcedure);
		return results;

		} 
	public async Task<Employee> GetEmployeeById(int Id)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "E");
			p.Add("@P_EmployeeID",Id);
		
		var results = await Connection.QueryAsync<Employee>("USP_Employee_Copy_2", p, commandType: CommandType.StoredProcedure);
		return results.FirstOrDefault();

		} 
	public async Task<List<Employee>> Getall_Employee(Employee TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "V");
		var results = await Connection.QueryAsync<Employee>("USP_Employee_Copy_2",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	public async Task<List<Employee>> Search_Employee(Employee TBL)
	{
				var p = new DynamicParameters();
		p.Add("@P_Action", "S");
			p.Add("@P_EmployeeID",TBL.EmployeeID);
		
		p.Add("@P_LastName",TBL.LastName);
		p.Add("@P_FirstName",TBL.FirstName);
		p.Add("@P_Qualification",TBL.Qualification);
		p.Add("@P_Address",TBL.Address);
		p.Add("@P_City",TBL.City);
		p.Add("@P_Gender",TBL.Gender);
		p.Add("@P_EmployeeDocument",TBL.EmployeeDocument);
		var results = await Connection.QueryAsync<Employee>("USP_Employee_Copy_2",p, commandType: CommandType.StoredProcedure);
		return results.ToList();

		} 
	
}
}
