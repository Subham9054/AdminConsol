using CodeGen.Repository.Repositories.Interfaces;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;
using CodeGen.Model.ProjectMaster;
using CodeGen.Model.SuccessMessage;


namespace CodeGen.Repository.Repository
{
    public class ProjectLinkServiceProvider: WizTestRepositoryBase,IProjectLinkServiceProvider
    {
      
        public ProjectLinkServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
       

          public string AddProjectLink(Project objProjectlink)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "A");
                dyParam.Add("P_NVCHPROJECTLINKNAME",objProjectlink.NVCHPROJECTLINKNAME.Trim());
                dyParam.Add("P_NVCHPROJECTLINKDESC",objProjectlink.NVCHPROJECTLINKDESC!=null ? objProjectlink.NVCHPROJECTLINKDESC.Trim(): objProjectlink.NVCHPROJECTLINKDESC);
                dyParam.Add("P_intCreatedBy",objProjectlink.INTCREATEDBY);
                //dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_MANAGE";
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
        public async Task<ViewProjectLink> GetAllActiveProjectLink()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VAA");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewPoject>();
                ViewProjectLink viewModel = new ViewProjectLink();
                viewModel.ViewProjectLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<ViewProjectLink> GetAllInActiveProjectLink()

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VAI");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewPoject>();
                ViewProjectLink viewModel = new ViewProjectLink();
                viewModel.ViewProjectLinkDetailsmodel = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string InactiveProjectLink(int id, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "I");
                dyParam.Add("P_INTPROJECTLINKID",id);
                dyParam.Add("P_intUpdatedBy",updatedby);
                dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_MANAGE";
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
        public string ActiveProjectLink(int id, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "MA");
                dyParam.Add("P_INTPROJECTLINKID",id);
                dyParam.Add("P_intUpdatedBy",updatedby);
                dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_MANAGE";
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
        public async Task<ViewProjectLink> GetById(int id)

        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VI");
                dyParam.Add("P_INTPROJECTLINKID",id);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ViewPoject>();
                ViewProjectLink viewModel = new ViewProjectLink();
                viewModel.ProjectLinkModelIdwise = t1.ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public string UpdateProjectLink(Project objProjectlink)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "E");
                dyParam.Add("P_INTPROJECTLINKID", objProjectlink.INTPROJECTLINKID);
                dyParam.Add("P_NVCHPROJECTLINKNAME", objProjectlink.NVCHPROJECTLINKNAME.Trim());
                dyParam.Add("P_NVCHPROJECTLINKDESC", objProjectlink.NVCHPROJECTLINKDESC != null ? objProjectlink.NVCHPROJECTLINKDESC.Trim() : objProjectlink.NVCHPROJECTLINKDESC);
                dyParam.Add("P_intUpdatedBy",objProjectlink.INTUPDATEDBY);
                //dyParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PROJECTLINK_MANAGE";
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
}
}
