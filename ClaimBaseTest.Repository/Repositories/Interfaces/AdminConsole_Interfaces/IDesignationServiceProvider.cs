using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGen.Model.DesignationMasterModel;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>
   public interface IDesignationServiceProvider
    {
        int AddDesignation(DesignationMaster model);
        Task<DesignationMasterModel> GetAllDesgination(int id);
        //Task<IEnumerable<Hierarchy>> GetHierarchyById(int id);
        string EditDesignation(DesignationMaster model);
        string MarkActive(DesignationMaster model);
        string MarkInactive(DesignationMaster model);
        Task<DesignationMasterModel> GetById(int id);
    }

}
