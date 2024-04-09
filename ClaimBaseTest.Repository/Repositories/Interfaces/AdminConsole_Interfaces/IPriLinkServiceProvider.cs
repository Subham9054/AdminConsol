using CodeGen.Model.PrimaryLink;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CodeGen.Repository.Repositories.Interfaces
{       
	  /// <summary>
        /// Gets the get connection.
        /// </summary>

    public interface IPriLinkServiceProvider
    {    
        //int ActivatePrimaryLink(Primary objPrimaryLink);
        //int AddPrimaryLink(Primary objPrimaryLink);
        //int DeletePrimaryLink(Primary objPrimaryLink);
        //string FillSLno(Primary objPrimaryLink);
        //IList<Primary> GetAllPrimaryLink(Primary objPrimaryLink);
        //int InactivatePrimaryLink(Primary objPrimaryLink);
        //int UpdatePrimaryLink(Primary objPrimaryLink);
        //int UpdateSlnoPrimaryLink(Primary objPrimaryLink);
        //IList<Primary> FillFunctionType();
        //IList<Primary> FillGlink(string UserId);
        //IList<Primary> FillGridview (int glinkid, char ActionCode,string Plinkid);
        //IList<Primary> FillGlinkReport(int Flag);
        //IList<Primary> FillPlinkReport(int GlinkId,int Flag);

        string AddPrimaryLink(PrimaryModel objPrimaryLink);
        Task<PrimaryModel> GetAllFunctionMaster();
        Task<PrimaryModel> GetAllPrimaryLink(int id);
        string MarkInactivePrimaryLink(PrimaryModel objPrimaryLink);
        Task<IEnumerable<UpdatePrimaryLink>> GetPrimaryLinkById(int id);
        string EditPrimaryLink(PrimaryModel objPrimaryLink);
        string MarkActivePrimaryLink(PrimaryModel objPrimaryLink);
        Task<PrimaryModel> GetAllPrimaryLinkByGlobalLink(PrimaryModel objPrimaryLink);
        string ModifySlnoPrimaryLink(int primaryId, int slno, int updatedby);
        int GetPrimaryLinkMaxId(int Glinkid);
        Task<PrimaryModel> GetAllProjectLink();
        Task<PrimaryModel> GetAllActiveGlobalLinkByProjectId(int ProjectId);
    }
}
