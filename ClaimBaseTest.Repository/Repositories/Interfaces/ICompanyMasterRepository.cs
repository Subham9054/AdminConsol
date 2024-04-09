using System.Collections.Generic;

using ClaimBaseTest.Model.Company;
namespace ClaimBaseTest.Repository.Repositories.Interfaces
{
  public interface ICompanyMasterRepository
  {
  	 
        Task<int> Insert_Company(Company TBL);
        Task<int> Delete_Company(int Id);
        Task<Company> GetCompanyById(int Id);
        Task<List<Company>> Search_Company(Company TBL);
        Task<List<Company>> Getall_Company(Company TBL);
}
}
