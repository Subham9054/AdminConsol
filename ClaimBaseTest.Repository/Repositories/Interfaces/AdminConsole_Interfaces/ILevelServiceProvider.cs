using CodeGen.Model.LevelMasterModel;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>
    public interface ILevelServiceProvider
    {
         string AddLevel(CreateLevelMaster objlevel);
        Task<CreateLevelMaster> GetAllLevelByHierarchyId(int id);
        Task<LevelMasterModel> GetAllLevel(int id);
        Task<IEnumerable<CreateLevelMaster>> GetLevelById(int id);
        string EditLevel(CreateLevelMaster objlevel);
        string MarkActive(LevelMasterModel objmodel);
        string MarkInactive(LevelMasterModel objmodel);
    }

}
