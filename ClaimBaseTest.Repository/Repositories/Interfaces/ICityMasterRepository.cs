using System.Collections.Generic;

using ClaimBaseTest.Model.City;
namespace ClaimBaseTest.Repository.Repositories.Interfaces
{
  public interface ICityMasterRepository
  {
  	 
        Task<int> Insert_City(City TBL);
        Task<int> Delete_City(int Id);
        Task<City> GetCityById(int Id);
        Task<List<City>> Search_City(City TBL);
        Task<List<City>> Getall_City(City TBL);
}
}
