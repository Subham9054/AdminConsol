using System.Collections.Generic;

using ClaimBaseTest.Model.Employee;
namespace ClaimBaseTest.Repository.Repositories.Interfaces
{
  public interface IEmpMasterRepository
  {
  	 
        Task<int> Insert_Employee(Employee TBL);
        Task<int> Delete_Employee(int Id);
        Task<Employee> GetEmployeeById(int Id);
        Task<List<Employee>> Search_Employee(Employee TBL);
        Task<List<Employee>> Getall_Employee(Employee TBL);
}
}
