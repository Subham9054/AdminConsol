using CodeGen.Model.HirerachyMaster;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>

    public interface IHierarchyServiceProviderRepository
    {    
         string AddHierarchy(Hierarchy objhierarchy);
        Task<HierarchyModel> GetAllHierarchy(int id);
        string EditHierarchy(Hierarchy objhierarchy);
        string MarkActive(Hierarchy objhierarchy);
        string MarkInactive(Hierarchy objhierarchy);
        Task<HierarchyModel> GetById(int id);
    }
}
