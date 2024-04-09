using ClaimBaseTest.Core;
using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using CodeGen.Model.GlobalLink;
//using AdConsole11.Repository.USP_DIST;
//using CodeGen.Model.PrimaryLink;
//using CodeGen.Model.PrimaryLink;
//using CodeGen.Model.PrimaryLink;
using CodeGen.Model.SuccessMessage;
using CodeGen.Model.User;
using CodeGen.Repository.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Repository.Repository
{
    public class MenueManageServiceProvider : WizTestRepositoryBase, IMenueManageProvider
    {
        object param = new object();
        public MenueManageServiceProvider(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        #region Tab Master
        public string AddTabMaster(ButtonTab objButton)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "ADD");
                dyParam.Add("P_TABNAME", objButton.TABNAME);
                dyParam.Add("P_BUTTONID", objButton.BUTTONID);
                dyParam.Add("P_INTFUNCTIONID", objButton.INTFUNCTIONID);
                dyParam.Add("P_EXTERNALURL", objButton.EXTERNALURL);
                dyParam.Add("P_OPENINNEWWINDOW", objButton.OPENINNEWWINDOW);
                dyParam.Add("P_INTCREATEDBY", objButton.INTCREATEDBY);
                dyParam.Add("P_INTCREATEDBY", objButton.INTCREATEDBY);
                dyParam.Add("P_INTSORTTAB", objButton.INTSORTNUM);
                //dyParam.Add("P_INTUPDATEDBY", objButton.INTUPDATEDBY);

                var query = "USP_MANAGE_TAB";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strOutput;
        }
        public async Task<List<ButtonTab>> GetAllTabMasterById(int TABID)
        {
            var p = new DynamicParameters();
            p.Add("@P_Action", "GETBYID");
            p.Add("@P_TABID", TABID);
            var result = await Connection.QueryAsync<ButtonTab>("USP_MANAGE_TAB", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public string UpdateTabMaster(ButtonTab objButton)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UPDATE");
                dyParam.Add("P_TABID", objButton.TABID);
                dyParam.Add("P_TABNAME", objButton.TABNAME);
                dyParam.Add("P_BUTTONID", objButton.BUTTONID);
                dyParam.Add("P_INTFUNCTIONID", objButton.INTFUNCTIONID);
                dyParam.Add("P_EXTERNALURL", objButton.EXTERNALURL);
                dyParam.Add("P_OPENINNEWWINDOW", objButton.OPENINNEWWINDOW);
                dyParam.Add("P_INTUPDATEDBY", objButton.INTUPDATEDBY);
                dyParam.Add("P_INTSORTTAB", objButton.INTSORTNUM);

                var query = "USP_MANAGE_TAB";
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
        public async Task<ButtonTab> GetAllButton()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "GETALL");
                var query = "USP_MANAGE_BUTTONTAB";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<ButtonMasterModel>();
                ButtonTab viewModel = new ButtonTab();
                viewModel.ButtonList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ButtonTab>> GetAllTabMaster()
        {
            var p = new DynamicParameters();
            p.Add("@P_Action", "GETALL");
            var result = await Connection.QueryAsync<ButtonTab>("USP_MANAGE_TAB", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public string DeleteTabMaster(int TABID)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "DELETE");
                dyParam.Add("@P_TABID", TABID);
                var query = "USP_MANAGE_TAB";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return strOutput;
        }
        public int GetTabMaxSortNo(int intsortnum)
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "SORTTAB");
                dyParam.Add("@P_INTSORTTAB", intsortnum);
                var query = "USP_MANAGE_TAB";
                var result = Connection.Query<int>(query, dyParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ModifySlnoTabMaster(int globalId, int slno, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "MS");
                dyParam.Add("P_TABID", globalId);
                dyParam.Add("P_INTSORTTAB", slno);
                dyParam.Add("P_INTUPDATEDBY", updatedby);
                var query = "USP_MANAGE_TAB";
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
        #endregion

        #region Added By Subham for Serial No Set for Button
        public string ModifySlnoButtonLink(int buttonid, int slno, int updatedby)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("@P_Action", "BGM");
                dyParam.Add("@P_BUTTONID", buttonid);
                dyParam.Add("@P_INTSORTBTN", slno);
                dyParam.Add("@P_INTUPDATEDBY", updatedby);
                var query = "USP_MANAGE_BUTTONTAB";
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
        public int Getbuttonmax(int intsortnum)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@P_Action", "SORTBTN");
                aParam.Add("@P_INTSORTBTN", intsortnum);
                var query = "USP_MANAGE_BUTTONTAB";
                var result = Connection.Query<int>(query, aParam, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion

        //------------------------------------------------------------------------------------------------------------
        #region Button Master
        public string AddButtonMaster(ButtonTab objButton)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "ADD");
                dyParam.Add("P_BUTTONNAME", objButton.BUTTONNAME);
                dyParam.Add("P_INTPLINKID", objButton.INTPLINKID);
                dyParam.Add("P_INTFUNCTIONID", objButton.INTFUNCTIONID);
                dyParam.Add("P_EXTERNALURL", objButton.EXTERNALURL);
                dyParam.Add("P_OPENINNEWWINDOW", objButton.OPENINNEWWINDOW);
                dyParam.Add("P_INTCREATEDBY", objButton.INTCREATEDBY);
                //dyParam.Add("P_INTUPDATEDBY", objButton.INTUPDATEDBY);

                var query = "USP_MANAGE_BUTTONTAB";
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
        public string UpdateButtonMaster(ButtonTab objButton)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "UPDATE");
                dyParam.Add("@P_BUTTONID", objButton.BUTTONID);
                dyParam.Add("P_BUTTONNAME", objButton.BUTTONNAME);
                dyParam.Add("P_INTPLINKID", objButton.INTPLINKID);
                dyParam.Add("P_INTFUNCTIONID", objButton.INTFUNCTIONID);
                dyParam.Add("P_EXTERNALURL", objButton.EXTERNALURL);
                dyParam.Add("P_OPENINNEWWINDOW", objButton.OPENINNEWWINDOW);
                //dyParam.Add("P_INTCREATEDBY", objButton.INTCREATEDBY);
                dyParam.Add("P_INTUPDATEDBY", objButton.INTUPDATEDBY);

                var query = "USP_MANAGE_BUTTONTAB";
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

        //Already exist in PriLinkServiceProvider.cs but for my requirement I changed the model.
        public async Task<ButtonTab> GetAllFunctionMaster()
        {
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "VF");
                dyParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_PRIMARYLINK_VIEW";
                var result = await Connection.QueryMultipleAsync(query, dyParam, commandType: CommandType.StoredProcedure);
                var t1 = await result.ReadAsync<FunctionMasterModel>();
                ButtonTab viewModel = new ButtonTab();
                viewModel.FunctionList = t1.AsList();
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ButtonTab>> GetAllPrimaryLinkByGlobalLink(int INTGLINKID)
        {
            var p = new DynamicParameters();
            p.Add("@P_Action ", "VG"); //dyParam.Add("P_Action", "VG");
            p.Add("@P_intGLinkId", INTGLINKID);
            var results = await Connection.QueryAsync<ButtonTab>("USP_CPKG_1_PRIMARYLINK_VIEW", p, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
        public async Task<List<ButtonTab>> GetAllButtonMaster()
        {
            var p = new DynamicParameters();
            p.Add("@P_Action", "GETALL");
            var result = await Connection.QueryAsync<ButtonTab>("USP_MANAGE_BUTTONTAB", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<ButtonTab>> GetAllButtonMasterById(int BUTTONID)
        {
            var p = new DynamicParameters();
            p.Add("@P_Action", "GETBYID");
            p.Add("@P_BUTTONID", BUTTONID);
            var result = await Connection.QueryAsync<ButtonTab>("USP_MANAGE_BUTTONTAB", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public string DeleteButtonMaster(int BUTTONID)
        {
            string strOutput = "";
            IEnumerable<SuccessMessage> SuccessMessages;
            try
            {
                var dyParam = new DynamicParameters();
                dyParam.Add("P_Action", "DELETE");
                dyParam.Add("@P_BUTTONID", BUTTONID);
                var query = "USP_MANAGE_BUTTONTAB";
                SuccessMessages = Connection.Query<SuccessMessage>(query, dyParam, commandType: CommandType.StoredProcedure);
                strOutput = SuccessMessages.AsList()[0].successid.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return strOutput;
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------
    }
}
