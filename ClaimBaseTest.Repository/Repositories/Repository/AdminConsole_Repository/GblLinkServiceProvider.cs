using CodeGen.Repository.Repositories.Interfaces;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;
using CodeGen.Model.GlobalLink;
using CodeGen.Model.SuccessMessage;

namespace CodeGen.Repository.Repository
{
    public class GblLinkServiceProvider: WizTestRepositoryBase,IGblLinkServiceProvider
    {
      
        public GblLinkServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
       

        public int ActivateGlobalLink(Global objGloballink)
        {
            throw new NotImplementedException();
        }

        //public string AddGlobalLink(Global objGloballink)
        //{
        //    string strOutput = "";
        //    IEnumerable<SuccessMessage> SuccessMessages;
        //    try
        //    {
        //        var dyParam = new DynamicParameters();
        //        dyParam.Add("P_Action","A");
        //        dyParam.Add("P_nvchGLinkName",objGloballink.GlobalLinkName.ToUpper());
        //        dyParam.Add("P_intCreatedBy", objGloballink.CreatedBy);
        //        dyParam.Add("P_intUpdatedBy", objGloballink.CreatedBy);
        //        dyParam.Add("P_intSortNum",  objGloballink.GintSortNum);
        //        dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
        //        var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
        //        SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
        //        strOutput = SuccessMessages.AsList()[0].successmessage.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Logger.LogError(ex, ex.Message, Model);
        //        throw ex;
        //    }
        //    return strOutput;
        //}
        public string AddGlobalLink(ViewGlobal objGloballink)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","A");
                dyParam.Add("P_nvchGLinkName",objGloballink.GlobalLinkName.Trim());
                dyParam.Add("P_intCreatedBy", objGloballink.CreatedBy);
                dyParam.Add("P_intUpdatedBy", objGloballink.CreatedBy);
                dyParam.Add("P_intSortNum", objGloballink.GintSortNum);
                dyParam.Add("P_INTPROJECTID",objGloballink.INTPROJECTLINKID);
                dyParam.Add("P_VCHICONCLASS",objGloballink.VCHICONCLASS);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
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
        public string EditGlobalLink(ViewGlobal objupdateGloballink)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "E");
                dyParam.Add("P_nvchGLinkName", objupdateGloballink.nvchGLinkName.Trim());
                dyParam.Add("P_VCHICONCLASS", objupdateGloballink.VCHICONCLASS.Trim());
                dyParam.Add("P_intGLinkId",objupdateGloballink.intGLinkID);
             
                dyParam.Add("P_intUpdatedBy",objupdateGloballink.updatedBy);
                dyParam.Add("P_INTPROJECTID",objupdateGloballink.INTPROJECTLINKID);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
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
        public string DeleteGlobalLink(int id, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "D");
                dyParam.Add("P_intGLinkId",id);
                dyParam.Add("P_intUpdatedBy",updatedby);
                //dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
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
        public async Task<ViewGlobalLink> GetAllActiveGlobalLink()
       
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VAA");
               // dyParam.Add("P_intStatus",status);
                dyParam.Add("cur",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewGlobal>();
                ViewGlobalLink viewModel = new ViewGlobalLink();
                viewModel.ViewGlobalLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
               //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<ViewGlobalLink> GetAllInActiveGlobalLink()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","VAI");
                // dyParam.Add("P_intStatus",status);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewGlobal>();
                ViewGlobalLink viewModel = new ViewGlobalLink();
                viewModel.ViewGlobalLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string ModifySlnoGlobalLink(int globalId,int slno, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "MS");
                dyParam.Add("P_intGLinkId",globalId);
                dyParam.Add("P_intSortNum",slno);
                dyParam.Add("P_intUpdatedBy",updatedby);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
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
        //public async Task<IEnumerable<Global>> GetGlobalLinkMaxId()
        public int GetGlobalLinkMaxId()
        {
            int GMaxId;
            IEnumerable<ViewGlobal> maxid;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "S");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                maxid= Connection.Query<ViewGlobal>(query, dyParam, commandType: CommandType.StoredProcedure);
                GMaxId =Convert.ToInt32( maxid.AsList()[0].GMAxid);
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
            return GMaxId;
        }
    
        public async Task<IEnumerable<ViewGlobal>> GetGlobalLinkById(int id)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","VI");
                dyParam.Add("P_intGLinkId",id);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                  var result = await Connection.QueryAsync<ViewGlobal>(query, dyParam, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<ViewGlobalLink> GetById(int id)

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","VI");
                dyParam.Add("P_intGLinkId",id);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewGlobal>();
                ViewGlobalLink viewModel = new ViewGlobalLink();
                viewModel.ViewGlobalLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<ViewGlobalLink> GetMaxId()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action","S");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewGlobal>();
                ViewGlobalLink viewModel = new ViewGlobalLink();
                viewModel.ViewGlobalLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string MarkActiveGlobalLink(int id, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "EI");
                dyParam.Add("P_intGLinkId",id);
                dyParam.Add("P_intUpdatedBy",updatedby);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_GLOBALLINK_MANAGE";
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

        //public int DeleteGlobalLink(Global objGloballink)
        //{
        //    throw new NotImplementedException();
        //}

        //public int EditGlobalLink(Global objGloballink)
        //{
        //    throw new NotImplementedException();
        //}

        //public IList<Global> GetAllGlobalLink(Global objGloballink)
        //{
        //    throw new NotImplementedException();
        //}

        public IList<Global> GetAllLocation(Global objGloballink)
        {
            throw new NotImplementedException();
        }

        //public string GetGlobalLinkById(Global objGloballink)
        //{
        //    throw new NotImplementedException();
        //}

        public IList<Global> GetGlobalLinkDetails(Global objGloballink)
        {
            throw new NotImplementedException();
        }

        public int GetMaxGlinkId()
        {
            throw new NotImplementedException();
        }

        public int InActivateGlobalLink(Global objGloballink)
        {
            throw new NotImplementedException();
        }

        public int UpdateSlno(Global objGloballink)
        {
            throw new NotImplementedException();
        }
}
}
