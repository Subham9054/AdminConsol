using ClaimBaseTest.Repository.BaseRepository;
using ClaimBaseTest.Repository.Factory;
using CodeGen.Repository.Repositories.Interfaces;
using System.Data;
using Dapper;
using CodeGen.Model.FunctionMasterModel;
using System;
using System.Collections.Generic;
using CodeGen.Model.SQLHelper;
using CodeGen.Model.DataBaseHelper;
using System.Data.SqlClient;
using CodeGen.Model.FunctionMasterNewModel;
namespace CodeGen.Repository.Repository
{
    public class FuncServiceProviderNew: WizTestRepositoryBase,IFuncServiceProvider
    { 
      
     #region Variable Declaration
        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[DataBaseHelper.ConnectionString].ConnectionString);
        object param = new object();
        //  int intOutput = 0;
        
        #endregion

        public FuncServiceProviderNew(IWizTestConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
      
         public int ActiveFunction(FunctionMasterModel objFunctionMaster)
        {  
            try
            {
                var p = new DynamicParameters();
                p.Add("@mvarchrActionCode",  objFunctionMaster.ActionCode);
                p.Add("@mvarintFunctionId", objFunctionMaster.FunctionId);
                p.Add("@mvarintCreatedBy", objFunctionMaster.CreatedBy);
                p.Add("@mvarvchOutPut", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                p.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var results = Connection.Execute("USP_CPKG_1_FUNCTION_MANAGE", p, commandType: CommandType.StoredProcedure);
                var cc = Convert.ToInt32(p.Get<string>("@P_Msg"));
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }

                      
        }

        public int AddFunction(FunctionMasterModel objFunctionMaster)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("@mvarchrActionCode", objFunctionMaster.ActionCode);
                aParam.Add("@mvarintFunctionId", objFunctionMaster.FunctionId);
                aParam.Add("@mvarvchFunctionName", objFunctionMaster.FunctionName.Trim());
                aParam.Add("@mvarvchFileName", objFunctionMaster.FileName.Trim());
                aParam.Add("@mvarvchDescription",  objFunctionMaster.Description);
                aParam.Add("@mvarvchAction1",  objFunctionMaster.FAdd);
                aParam.Add("@mvarvchAction2",  objFunctionMaster.FView);
                aParam.Add("@mvarvchAction3",  objFunctionMaster.FManage);
                aParam.Add("@mvarbitMailR", objFunctionMaster.MailR);
                aParam.Add("@mvarbitFreeBees",  objFunctionMaster.FreeBees);
                aParam.Add("@mvarvchPortletFile", objFunctionMaster.PortletFile);
                aParam.Add("@mvarintCreatedBy",  objFunctionMaster.CreatedBy);
                aParam.Add("@mvarvchOutPut", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("@P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_FUNCTION_MANAGE";
                var results = Connection.Execute(query, aParam, commandType: CommandType.StoredProcedure);
                return results;
                             
            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }   
        }

        public int DeleteFuncImage(string actionCode, int funcId)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("mvarchrActionCode", actionCode);
                aParam.Add("mvarintFunctionId",  funcId);
                aParam.Add("mvarvchOutPut", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_FUNCTION_MANAGE";
                var results = Connection.Execute(query, aParam, commandType: CommandType.StoredProcedure);
                return results;

            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }           
        }

        public int EditFunction(FunctionMasterModel objFunctionMaster)
        {
            try
            {
                var aParam = new DynamicParameters();
                aParam.Add("mvarchrActionCode", objFunctionMaster.ActionCode);
                aParam.Add("mvarintFunctionId", objFunctionMaster.FunctionId);
                aParam.Add("mvarvchFunctionName",  objFunctionMaster.FunctionName.Trim());
                aParam.Add("mvarvchFileName", objFunctionMaster.FileName.Trim());
                aParam.Add("mvarvchDescription",  objFunctionMaster.Description);
                aParam.Add("mvarvchAction1",  objFunctionMaster.FAdd);
                aParam.Add("mvarvchAction2", objFunctionMaster.FView);
                aParam.Add("mvarvchAction3",  objFunctionMaster.FManage);
                aParam.Add("mvarbitMailR",  objFunctionMaster.MailR);
                aParam.Add("mvarbitFreeBees",  objFunctionMaster.FreeBees);
                aParam.Add("mvarvchPortletFile", objFunctionMaster.PortletFile);
                aParam.Add("mvarintCreatedBy",  objFunctionMaster.CreatedBy);
                aParam.Add("mvarvchOutPut", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                //aParam.Add("P_Msg", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var query = "USP_CPKG_1_FUNCTION_MANAGE";
                var results = Connection.Execute(query, aParam, commandType: CommandType.StoredProcedure);
                return results;
            }
            catch (Exception exception)
            {
                throw new Exception("Execution Failed", exception);
            }     
        }

        public IList<FunctionMasterModel> GetAllFunction(FunctionMasterModel objFunctionMaster)
        {
            List<FunctionMasterModel> list = new List<FunctionMasterModel>();
            var vParam = new DynamicParameters();
            vParam.Add("p_chrActionCode",  objFunctionMaster.ActionCode);
            vParam.Add("p_intFunctionId", objFunctionMaster.FunctionId);
            vParam.Add("p_vchFunName",   objFunctionMaster.FunctionName);
            vParam.Add("p_vchSearchText",   objFunctionMaster.FunctionName);
            vParam.Add("p_intFlag",  objFunctionMaster.Flag);
            vParam.Add("cur", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            
            var query = "USP_CPKG_1_FUNCTION_VIEW";
            var result = Connection.Query<FunctionMasterNewModel>(query, vParam, commandType: CommandType.StoredProcedure);
            foreach (var funMaster in result.AsList())
            {
                FunctionMasterModel item = new FunctionMasterModel
                {
                    RowNo = funMaster.RowNo,// Convert.ToInt32(reader["RowNo"].ToString()),
                    FunctionId = funMaster.intFunctionId,//Convert.ToString(reader["intFunctionId"]),
                    FunctionName = funMaster.vchFunction,//Convert.ToString(reader["vchFunction"]),
                    FileName = funMaster.vchFileName,//Convert.ToString(reader["vchFileName"]),
                    Description = funMaster.vchDescription,// Convert.ToString(reader["vchDescription"]),
                    FAdd = funMaster.vchAction1,// Convert.ToString(reader["vchAction1"]),
                    FView = funMaster.vchAction2,//Convert.ToString(reader["vchAction2"]),
                    FManage = funMaster.vchAction3,//Convert.ToString(reader["vchAction3"]),
                    MailR = funMaster.bitMailSend,//Convert.ToInt32(reader["bitMailSend"]),
                    FreeBees = funMaster.bitFreebees,// Convert.ToInt32(reader["bitFreebees"]),
                    PortletFile =funMaster.vchportletfile,// Convert.ToString(reader["vchportletfile"])
                };
                list.Add(item);
            }
          
               
            return list;
        }

        public string GetFunctionData(int intFuncId)
        {
            throw new NotImplementedException();
        }

        public IList<FunctionMasterModel> GetFunctionDetails(FunctionMasterModel objFunctionMaster)
        {
            throw new NotImplementedException();
        }

        public IList<FunctionMasterModel> GetFunctionId(string userId)
        {
            throw new NotImplementedException();
        } 
   }
}
