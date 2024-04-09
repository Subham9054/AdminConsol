using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGen.Model.FunctionMasterModel;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>
   public interface IFuncServiceProvider
    {
       int ActiveFunction(FunctionMasterModel objFunctionMaster);
        int AddFunction(FunctionMasterModel objFunctionMaster);
        int EditFunction(FunctionMasterModel objFunctionMaster);
        IList<FunctionMasterModel> GetAllFunction(FunctionMasterModel objFunctionMaster);
        int DeleteFuncImage(string actionCode, int funcId);
        IList<FunctionMasterModel> GetFunctionId(string userId);
        string GetFunctionData(int intFuncId);
        IList<FunctionMasterModel> GetFunctionDetails(FunctionMasterModel objFunctionMaster);
    }

}
