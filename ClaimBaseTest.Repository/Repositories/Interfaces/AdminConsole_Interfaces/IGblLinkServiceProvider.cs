using CodeGen.Model.GlobalLink;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>

    public interface IGblLinkServiceProvider
    {    
         int ActivateGlobalLink(Global objGloballink);
        //int AddGlobalLink(Global objGloballink);
       // int DeleteGlobalLink(Global objGloballink);
       // int EditGlobalLink(Global objGloballink);
      //  IList<Global> GetAllGlobalLink(Global objGloballink);
       // string GetGlobalLinkById(Global objGloballink);
        IList<Global> GetGlobalLinkDetails(Global objGloballink);
        int InActivateGlobalLink(Global objGloballink);
        int UpdateSlno(Global objGloballink);
        //int GetMaxGlinkId();
        IList<Global> GetAllLocation(Global objGloballink);
        //---------------------------------------------------
        //string AddGlobalLink(Global objGloballink);
        string AddGlobalLink(ViewGlobal objGloballink);
        Task<IEnumerable<ViewGlobal>> GetGlobalLinkById(int id);
    
        Task<ViewGlobalLink> GetAllActiveGlobalLink();
        Task<ViewGlobalLink> GetAllInActiveGlobalLink();
        //string EditGlobalLink(ViewGlobal objGloballink);
        string EditGlobalLink(ViewGlobal objGloballink);
        string DeleteGlobalLink(int id, int updatedby);
        string MarkActiveGlobalLink(int id,int updatedby);
        string ModifySlnoGlobalLink(int globalId, int slno, int updatedby);
        int GetGlobalLinkMaxId();
        Task<ViewGlobalLink> GetById(int id);
        Task<ViewGlobalLink> GetMaxId();
    }
}
