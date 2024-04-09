using CodeGen.Repository.Repositories.Interfaces;
using CodeGen.Model.SuccessMessage;
using CodeGen.Model.Set_Permission;
using System.Data;
using Dapper;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using System.Linq;
using CodeGen.Model.User;
using CodeGen.Model.GlobalLink;

namespace CodeGen.Repository.Repository
{
    public class SetPermissionServiceProvider : WizTestRepositoryBase, ISetPermissionServiceProvider
    {
        public SetPermissionServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<SetPermissionModel> GetAllPrimaryLinkByGlobalLink(SetPermissionModel objpermission)
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VGP");
                dyParam.Add("P_INTDESINATIONID", objpermission.DesignId);
                dyParam.Add("P_INTPROJECTID", objpermission.INTPROJECTLINKID);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string SetPermissionLink_Designation(int designationId, int Plinkid, int Intaccess, int user, int projectId)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "AED");
                dyParam.Add("P_INTDESINATIONID", designationId);
                dyParam.Add("P_intpLinkId", Plinkid);
                dyParam.Add("P_TINACCESS", Intaccess);
                dyParam.Add("P_intuser", user);
                dyParam.Add("P_INTPROJECTID", projectId);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_MANAGE";
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
        public async Task<SetPermissionModel> GetAllPrimaryLinkByGlobalLinkUserwise(SetPermissionModel objpermission)
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VUGP");
                dyParam.Add("P_INTUSERID", objpermission.UserId);
                dyParam.Add("P_INTPROJECTID", objpermission.INTPROJECTLINKID);
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string SetPermissionLink_User(int userId, int Plinkid, int Intaccess, int user, int projectId)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "AEU");
                dyParam.Add("P_INTUSERID", userId);
                dyParam.Add("P_intpLinkId", Plinkid);
                dyParam.Add("P_TINACCESS", Intaccess);
                dyParam.Add("P_intuser", user);
                dyParam.Add("P_INTPROJECTID", projectId);

                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_MANAGE";
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
        public string DeletePermissionLink_Designation(int DesignationId, int projectId)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "DPD");
                dyParam.Add("P_INTDESINATIONID", DesignationId);
                dyParam.Add("P_INTPROJECTID", projectId);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_MANAGE";
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
        public string DeletePermissionLink_User(int userId, int projectId)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "DPU");
                dyParam.Add("P_INTUSERID", userId);
                dyParam.Add("P_INTPROJECTID", projectId);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_MANAGE";
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
        public async Task<SetPermissionModel> GetAllUser()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VU");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<UserDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.UserList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> GetAllRole()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "RI");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<RoleDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.RoleList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public string RemovePermissionList_User(int userId, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "RUP");
                dyParam.Add("P_INTUSERID", userId);
                dyParam.Add("P_intuser", updatedby);
                //dyParam.Add("P_Msg",dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_MANAGE";
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
        //For retrieve all Designation
        public async Task<SetPermissionModel> GetAllDesignation()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VD");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<DesignationDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.DesignationList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        //For retrieve all link.
        public IList<LinkAccess> GetLinkAccessByUserId(int UserId, int ProjectId, int desgid)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@p_action ", "VU");
                aParam.Add("p_intintuserid", UserId);
                aParam.Add("p_intprojectid", ProjectId);
                aParam.Add("p_intdesignationid", desgid);
                //aParam.Add("cur", OracleDbType.RefCursor, direction: ParameterDirection.Output);
                var query = "USP_CPKG_1_USERMASTER_VIEW";
                var result = Connection.Query<LinkAccess>(query, aParam, commandType: CommandType.StoredProcedure);
                return result.ToList();

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #region For View Components
        public IList<ButtonTab1> GetButtonWithTabAccess(string plinkid, string btnid, int useridlClaims)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@P_Action", "BTN");
                aParam.Add("@P_INTPLINKID", plinkid);
                aParam.Add("@P_BUTTONID", btnid);
                aParam.Add("@P_USERID", useridlClaims);
                var query = "USP_MANAGE_BUTTONTAB";
                var result = Connection.Query<ButtonTab1>(query, aParam, commandType: CommandType.StoredProcedure);
                return (IList<ButtonTab1>)result.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        //public IList<ButtonTab> GetButtonWithTabAccess(string plinkid, string btnid)
        //{
        //    try
        //    {
        //        var aParam = new DynamicParameters();
        //        aParam.Add("@P_Action", "BTN");
        //        aParam.Add("@P_INTPLINKID", plinkid);
        //        aParam.Add("@P_BUTTONID", btnid);
        //        var query = "USP_MANAGE_BUTTONTAB";
        //        var result = Connection.Query<ButtonTab>(query, aParam, commandType: CommandType.StoredProcedure);
        //        return (IList<ButtonTab>)result.ToList();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //}
        public string GetButtondefaultTab(string plinkValue)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@P_Action", "BTNID");
                aParam.Add("@P_INTPLINKID", plinkValue);
                var query = "USP_MANAGE_BUTTONTAB";
                var result = Connection.Query<string>(query, aParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public string GetdefaultTab(string plinkValue,string btnid)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@P_Action", "TABID");
                aParam.Add("@P_INTPLINKID", plinkValue);
                aParam.Add("@P_BUTTONID", btnid);
                var query = "USP_MANAGE_BUTTONTAB";
                var result = Connection.Query<string>(query, aParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion

        #region Added By Subham For User Set Permission
        public async Task<SetPermissionModel> GetPrimaryLinkByGlobalLink(int Globid)
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "PB");
                dyParam.Add("@P_intGLinkId", Globid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> GetButtonByPrimaryLink(int Primid)
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "BB");
                dyParam.Add("@@P_intPLinkId", Primid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> GetAllDetails(int Projectid, int globalLinkId, int primaryLinkId, int buttonId)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "RPT");
                dyParam.Add("@P_INTPROJECTID", Projectid);
                dyParam.Add("@P_intGLinkId", globalLinkId);
                dyParam.Add("@P_intPLinkId", primaryLinkId);
                dyParam.Add("@P_BUTTONID", buttonId);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> GetUserId(int Projectid, int globalLinkId, int primaryLinkId, int buttonId)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UIG");
                dyParam.Add("@P_INTPROJECTID", Projectid);
                dyParam.Add("@P_intGLinkId", globalLinkId);
                dyParam.Add("@P_intPLinkId", primaryLinkId);
                dyParam.Add("@P_BUTTONID", buttonId);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> GetAllUserID()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UI");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_SETPERMISSION_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<UserDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.UserList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<int> SaveUserdetail(UserSet userSet)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("@P_Action", "INS");
                dyParam.Add("@P_INTPROJECTID", userSet.INTPROJECTLINKID);
                dyParam.Add("@P_intGLinkId", userSet.INTGLOBALLINKID);
                dyParam.Add("@P_intPLinkId", userSet.INTPRIMARYTLINKID);
                dyParam.Add("@P_BUTTONID", userSet.INTBUTTONLINKID);
                dyParam.Add("@TabName", userSet.TabName);
                dyParam.Add("@P_TABID", userSet.TABID);
                dyParam.Add("@P_USER", userSet.UserId);
                dyParam.Add("@CreatedBy", 1);
                dyParam.Add("@Setpermission", 1);

                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }


        public async Task<int> RemoveUserdetail(UserSet userSet)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "DEL");
                dyParam.Add("@P_Updatedby", 1);
                dyParam.Add("@P_USER", userSet.UserId);
             //   dyParam.Add("@P_Role", userSet.RoleId);
                dyParam.Add("@P_TABID", userSet.TABID);
                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<int> DeleteUID(int userid)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UID");
                dyParam.Add("@P_USER", userid);
               // dyParam.Add("@P_TABID", tabid);
                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<int> DeleteRID(int roleId)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UIR");
                dyParam.Add("@P_Role", roleId);
                //dyParam.Add("@P_TABID", tabid);
                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }


        public async Task<int> UpdateUserdetail(UserSet userSet)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("@P_Action", "UPD");
                dyParam.Add("@P_INTPROJECTID", userSet.INTPROJECTLINKID);
                dyParam.Add("@P_intGLinkId", userSet.INTGLOBALLINKID);
                dyParam.Add("@P_intPLinkId", userSet.INTPRIMARYTLINKID);
                dyParam.Add("@P_BUTTONID", userSet.INTBUTTONLINKID);
                dyParam.Add("@P_TABID", userSet.TABID);
                dyParam.Add("@TabName", userSet.TabName);
                dyParam.Add("@P_USER", userSet.UserId);
                dyParam.Add("@P_Role", 0);
                dyParam.Add("@CreatedBy", 1);
                dyParam.Add("@Setpermission", 1);
                dyParam.Add("@SelectedUserIds", userSet.UserIds);
                dyParam.Add("@SelectedRoleIds", userSet.RoleIds);
                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<int> UpdateRoledetail(UserSet userSet)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                var dyParam = new DynamicParameters();
                dyParam.Add("@P_Action", "UPDR");
                dyParam.Add("@P_INTPROJECTID", userSet.INTPROJECTLINKID);
                dyParam.Add("@P_intGLinkId", userSet.INTGLOBALLINKID);
                dyParam.Add("@P_intPLinkId", userSet.INTPRIMARYTLINKID);
                dyParam.Add("@P_BUTTONID", userSet.INTBUTTONLINKID);
                dyParam.Add("@P_TABID", userSet.TABID);
                dyParam.Add("@TabName", userSet.TabName);
                //dyParam.Add("@P_USER",0);
                dyParam.Add("@P_Role", userSet.RoleId);
                dyParam.Add("@CreatedBy", 1);
                dyParam.Add("@Setpermission", 1);
                var query = "USP_SET_PERMISSION";
                var results = await Connection.ExecuteAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> bindUserddl(int projid, int globid, int plid, int btnid, int tabid)
        {
            try
            {

                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "BDR");
                dyParam.Add("@P_INTPROJECTID", projid);
                dyParam.Add("@P_intGLinkId", globid);
                dyParam.Add("@P_intPLinkId", plid);
                dyParam.Add("@P_BUTTONID", btnid);
                //dyParam.Add("@P_TABID", tabid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<UserDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.UserList = t1.ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<SetPermissionModel> bindroleddl(int projid, int globid, int plid, int btnid,int tabid)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "BDR");
                dyParam.Add("@P_INTPROJECTID", projid);
                dyParam.Add("@P_intGLinkId", globid);
                dyParam.Add("@P_intPLinkId", plid);
                dyParam.Add("@P_BUTTONID", btnid);
                //dyParam.Add("@P_TABID",tabid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<SetPermissionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.GlobalPrimaryList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public async Task<SetPermissionModel> bindrole()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "BR");
                //dyParam.Add("@P_TABID",tabid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<RoleDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.RoleList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }
        public async Task<SetPermissionModel> bindFunction()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "BF");
                //dyParam.Add("@P_TABID",tabid);
                // dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_SET_PERMISSION";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<FunctionDetails>();
                SetPermissionModel viewModel = new SetPermissionModel();
                viewModel.FunctionList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, ex.Message, componentDomain);
                throw ex;
            }
        }

        public string SaveRolefunction(RoleSet roleSet)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("@P_Action", "INSR");
                dyParam.Add("@P_Role", roleSet.RoleId);
                dyParam.Add("@Selectedfunctionids", roleSet.FunctionIds);
                dyParam.Add("@Createdby", 1);
                dyParam.Add("@Setpermission", 1);
                var query = "USP_SET_PERMISSION";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strOutput;
        }
        #endregion

    }
}
