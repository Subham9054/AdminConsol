using CodeGen.Repository.Repositories.Interfaces;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;
using CodeGen.Model.PrimaryLink;
using CodeGen.Model.SuccessMessage;
using CodeGen.Model.ProjectMaster;
using CodeGen.Model.GlobalLink;


namespace CodeGen.Repository.Repository
{
    public class PriLinkServiceProvider: WizTestRepositoryBase,IPriLinkServiceProvider
    {
	  #region Variable Declaration        
        object param = new object();
      //  int intOutput;
        #endregion

      
        public PriLinkServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
       

           public string AddPrimaryLink(PrimaryModel objPrimary)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "A");
                dyParam.Add("P_nvchPLinkName", objPrimary.PlinkName);
                dyParam.Add("P_intGLinkId", objPrimary.GlinkId);
                dyParam.Add("P_intFunctionId", objPrimary.FunctionId);
                dyParam.Add("P_intCreatedBy",objPrimary.CreatedBy);
                dyParam.Add("P_intUpdatedBy", objPrimary.UpdatedBy);
                dyParam.Add("P_INTSLNO", objPrimary.INTSLNO);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLLINK_MANAGE";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
            return strOutput;
        }
        public string MarkInactivePrimaryLink(PrimaryModel objPrimary)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "I");
                dyParam.Add("P_intUpdatedBy", objPrimary.UpdatedBy);
                dyParam.Add("P_mvarintFunctionId", objPrimary.INTPLINKID);

                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLLINK_MANAGE";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
            return strOutput;
        }
        public async Task<PrimaryModel> GetAllFunctionMaster()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VF");
                dyParam.Add("cur",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<FunctionMasterModel>();
                PrimaryModel viewModel = new PrimaryModel();
                viewModel.FunctionList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<PrimaryModel> GetAllPrimaryLink(int id)

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","V");
                dyParam.Add("P_intStatus",id);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<PrimaryLink>();
                PrimaryModel viewModel = new PrimaryModel();
                viewModel.PrimaryLinkList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<IEnumerable<UpdatePrimaryLink>> GetPrimaryLinkById(int id)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VI");
                dyParam.Add("P_intPLinkId",id);
                dyParam.Add("cur",  dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
               
                var result = await Connection.QueryAsync<UpdatePrimaryLink>(query, dyParam, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public string EditPrimaryLink(PrimaryModel objPrimary)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","E");
                dyParam.Add("P_mvarintFunctionId",objPrimary.INTPLINKID);
                dyParam.Add("P_nvchPLinkName",objPrimary.PlinkName);
                dyParam.Add("P_intGLinkId",objPrimary.INTGLINKID);
                dyParam.Add("P_intFunctionId",objPrimary.INTFUNCTIONID);
                dyParam.Add("P_intUpdatedBy",objPrimary.UpdatedBy);
                //dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLLINK_MANAGE";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
            return strOutput;
        }
        public string MarkActivePrimaryLink(PrimaryModel objPrimary)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "EA");
                dyParam.Add("P_intUpdatedBy",objPrimary.UpdatedBy);
                dyParam.Add("P_mvarintFunctionId",objPrimary.INTPLINKID);

                //dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLLINK_MANAGE";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
            return strOutput;
        }
        public async Task<PrimaryModel> GetAllPrimaryLinkByGlobalLink(PrimaryModel objPrimary)

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","VG");
                dyParam.Add("P_intGLinkId", objPrimary.GlinkId);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<PrimaryLink>();
                PrimaryModel viewModel = new PrimaryModel();
                viewModel.PrimaryLinkList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public string ModifySlnoPrimaryLink(int primaryId, int slno, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "S");
                dyParam.Add("P_intUpdatedBy",updatedby);
                dyParam.Add("P_mvarintFunctionId",primaryId);
                dyParam.Add("P_INTSLNO",slno);
                dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLLINK_MANAGE";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successmessage.ToString();
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, Model);
                throw ex;
            }
            return strOutput;
        }
        public int GetPrimaryLinkMaxId(int Glinkid)
        {
            int INTSLNO;
            IEnumerable<PrimaryModel> maxid;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","S");
                dyParam.Add("P_intGLinkId",Glinkid);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                maxid = Connection.Query<PrimaryModel>(query, dyParam, commandType: CommandType.StoredProcedure);
                INTSLNO = Convert.ToInt32(maxid.AsList()[0].INTSLNO);
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
            return INTSLNO;
        }
        public async Task<PrimaryModel> GetAllProjectLink()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","VP");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewPoject>();
                PrimaryModel viewModel = new PrimaryModel();
                viewModel.ViewProjectLinkList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<PrimaryModel> GetAllActiveGlobalLinkByProjectId(int ProjectId)

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "GP");
                dyParam.Add("P_INTPROJECTID", ProjectId);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewGlobal>();
                PrimaryModel viewModel = new PrimaryModel();
                viewModel.ViewGlobalLinkDetailsByProjectId = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        
}
}
}
